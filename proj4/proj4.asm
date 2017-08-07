COMMENT#
************************************************************************************
Name:		Matthew Humphrey
Program:  	Proj4.asm
Class:		CSCI 2160-001
Date:		11/10/2015
Purpose:	To test the linked list

************************************************************************************  
#
	.486
	.model flat
	.stack 100h
	
	node struct
		keyValue dword ?
		recDesc byte 24 dup (?)
		next dword ?
	node ends
	
	ExitProcess PROTO Near32 stdcall, dwExitCode:dword  ;capitalization not necessary
	ascint32	PROTO Near32 stdcall, lpStringHolding:dword  ;returns int in EAX
	intasc32	PROTO Near32 stdcall, lpStringToHold:dword, dVal:dword
	intasc32Comma PROTO Near32 stdcall, lpStringToHold:dword, dVal:dword	
	getstring	PROTO Near32 stdcall, lpStringToHold:dword, dNumChars:dword
	putstring	PROTO Near32 stdcall, lpStringToDisplay:dword
	getche		PROTO Near32 stdcall
	getch		PROTO Near32 stdcall
	putch		PROTO Near32 stdcall, bVal:byte	
	createPicture PROTO Near32 stdcall, lpDatatable:dword, number:dword
	delete 		PROTO Near32 stdcall, lpStNode:dword, lpAvail:dword, nodeToDelete:dword,pred:dword, succ:dword
	ini 		PROTO Near32 stdcall, lpDatatable:dword, lpStNode:dword, lpAvail:dword, lpPred:dword, lpSucc:dword, num:dword	
	traverse	PROTO Near32 stdcall, stNode:dword
	find		PROTO Near32 stdcall, stNode:dword,keyvalue:dword,lpPred:dword, lpSucc:dword
	getNode		PROTO Near32 stdcall,lpAvail:dword
	insert		PROTO Near32 stdcall,lpStNode:dword,nodeToAdd:dword,pred:dword,succ:dword
	
	.data
availableNode	dword	? 
avail	dword	dataTable+0C0h	;the next available location ****
found dword	? 					;-1 if not in the table, otherwise the address of found value
stNode dword dataTable + 20h	;offset of the first value in the table
pred	dword	?				;the predecessor’s offset of value to look for
succ	dword	?				;the successor’s address of the value to
								; look for
dummy	byte 8 dup(?)
dataTable	label	dword
node0	node <32,"George Washington ", -1>
node1	node <15,"Benjamin Franklin ",dataTable + 0A0h>
node2	node <27,"Thomas Jefferson ",dataTable + 80h>
node3	node <25,"Abraham Lincoln ",dataTable + 40h>
node4	node <30,"John Quincy Adams ",dataTable + 0h>
node5	node <19,"Andrew Jackson    ",dataTable + 60h>
node6	node <0," ",dataTable + 0E0h>
node7	node <0h," ",dataTable + 100h>
node8	node <0h," ",-1>

strError	byte	10,13,"Error something went wrong",0
strFound	byte	10,13,"That key was found",0
strNotFound	byte	10,13,"Key cannot be found.",0
strPrompt1	byte	10,13,"Type desired code (T,F,N,C,I,D,Q): ",0
strPrompt2	byte	10,13,"Type L for linked list, A for available nodes: ",0
strPrompt3	byte	10,13,"Enter desired key value: ",0
strPrompt4	byte	10,13,"Enter desired amount of nodes to initialize: ",0
strPrompt5	byte	10,13,"Enter desired amount of rows to display: ",0
strAddrKey	byte	10,13,"The address of the entry with the key is ",0
charEntered	byte	?
strTemp		byte	100 dup(?)
strNewLine	byte	10,13,0
strHeader1	byte	10,13,9,"Name: ",9,"Matthew Humphrey",0
strHeader2	byte	10,13,9,"Class: ",9,"CSCI 2160-001",0
strHeader3	byte	10,13,9,"Date: ",9,"11/24/2015",0
strHeader4	byte	10,13,9,"Project: ",9,"4",0
strEnterDesc byte	10,13,"Enter a description for the node: ",10,0
strOutOfNodes byte	10,13,"Out of nodes",0

	.code
_start:
	MOV	ax,0							;instruction used to stop the debugger
	
	ASSUME eax: sdword					;so that eax will work as an sdword
	INVOKE putstring, ADDR strHeader1	;tell the name of the creator of the program
	INVOKE putstring, ADDR strHeader2	;tell the class name
	INVOKE putstring, ADDR strHeader3	;tell the date the project was created
	INVOKE putstring, ADDR strHeader4	;tell the project number
	
	MOV EBX,sizeOf node					;ebx = the size of a node
	LEA EDX,datatable					;so that addresses in datatable can be accessed
	ASSUME EDX: ptr node				;ecx can now be used as a node

	.WHILE AL != 'Q' && AL != 'q'		;so that the menu will come back up
	INVOKE putstring, ADDR strPrompt1	;ask for input
	INVOKE getche 						;get a character from the keyboard
	
	.IF AL == 'T' || AL == 't'			;if the user entered a t
	INVOKE putstring, ADDR strPrompt2	;asks the user which they wish to tranverse
	INVOKE getche 						;get a character from the keyboard
	INVOKE putstring, ADDR strNewLine	;jump down a line
	
	.IF AL == 'L' || AL == 'l'			;if they choose a linked list
	INVOKE traverse, stNode				;show the traversal of the datatable
	.ELSEIF AL == 'A' || AL == 'a'		;if they choose to traverse the available nodes
	INVOKE traverse, avail				;show the traversal of the datatable
	.ENDIF
	
	.IF eax == -1						;checks if traverse failed
	INVOKE putstring,ADDR strError		;display an error message
	.ENDIF
	
	.ELSEIF AL == 'F' || AL == 'f'				;if the user entered F
	INVOKE putstring, ADDR strPrompt3			;ask to enter a key to find
	INVOKE getstring, ADDR strTemp, 10			;get a value from the keyboard
	INVOKE ascint32, ADDR strTemp				;EAX = strTemp
	INVOKE find, stNode,EAX,ADDR pred,ADDR succ	;looks for a node
	
	.IF eax == -1								;checks if the node was found
	INVOKE putstring,ADDR strNotFound			;tells user it was not found
	.ELSE 
	INVOKE putstring,ADDR strFound				;tells the user it was found
	.ENDIF
	
	.ELSEIF AL == 'N'||AL == 'n'				;if the user entered an n
	INVOKE putstring, ADDR strPrompt4			;ask for the amount of rows to inialize
	INVOKE getstring, ADDR strTemp, 12			;temporarily hold the keyValue in strTemp
	INVOKE ascint32, ADDR strTemp				;turn the string into a number
	INVOKE ini, ADDR dataTable, ADDR stNode, ADDR avail, ADDR pred, ADDR succ, eax		;initialize the amount of rows asked for
	
	.ELSEIF AL == 'D'||AL == 'd'									;if the user entered a d
	INVOKE getNode, ADDR avail										;get an available node eax
	MOV EDX, eax													;so that EDX holds the address of the node
	
	INVOKE putstring, ADDR strPrompt3								;ask for the keyValue to put into the new node
	INVOKE getstring, ADDR strTemp, 12								;temporarily hold the keyValue in strTemp
	INVOKE ascint32, ADDR strTemp									;turn the string into a number
	MOV (node ptr [EDX]).keyValue, EAX								;EDX.next = eax = strTemp
	INVOKE find, stNode,EAX,ADDR pred,ADDR succ						;looks for a where the keyValue should belong
	
	.IF EAX != -1													;make sure eax does not equal to -1
	INVOKE delete, ADDR stNode,ADDR avail,EDX,pred,succ				;inserts the new node
	.ENDIF
	
	.ELSEIF AL == 'C'||AL == 'c'									;if the user entered a c
	INVOKE putstring, ADDR strPrompt5								;ask for the amount of rows to inialize
	INVOKE getstring, ADDR strTemp, 12								;temporarily hold the keyValue in strTemp
	INVOKE ascint32, ADDR strTemp									;turn the string into a number
	INVOKE createPicture, ADDR dataTable,eax						;initialize the amount of rows asked for
	
	.ELSEIF AL == 'I'||AL == 'i'									;if the user choose to insert a new node
	INVOKE getNode, ADDR avail										;get an available node eax
	MOV EDX, eax													;so that EDX holds the address of the node
	
	.IF EDX != -1													;make sure there was a node
	INVOKE putstring, ADDR strPrompt3								;ask for the keyValue to put into the new node
	INVOKE getstring, ADDR strTemp, 12								;temporarily hold the keyValue in strTemp
	INVOKE ascint32, ADDR strTemp									;turn the string into a number
	MOV (node ptr [EDX]).keyValue, EAX								;EDX.next = eax = strTemp
	INVOKE find, stNode,EAX,ADDR pred,ADDR succ						;looks for a where the keyValue should belong
	
	.IF EAX == -1 													;make sure eax does not equal to -1
	INVOKE putstring, ADDR strEnterDesc								;prompts the user to enter the desc of the node
	INVOKE getstring, ADDR (node ptr [EDX]).recDesc, 24				;gets the value into the recDesc
	INVOKE insert, ADDR stNode,EDX,pred,succ						;inserts the new node

	.ELSE
	INVOKE putstring,ADDR strError									;tell the user that there was an error
	.ENDIF
	
	.ELSE
	INVOKE putstring, ADDR strOutOfNodes							;tells the user it is out of empty nodes
	.ENDIF
	
	.ENDIF
	.ENDW 
	
	INVOKE ExitProcess,0
	PUBLIC _start	
	END