;Name:		Matthew Humphrey
;Program:  	linkedlist.asm
;Class:		CSCI 2160-001
;Date:		11/10/2015
; Purpose:	To build methods to use a linked list
;
;************************************************************************************  
	.486
	.model flat
	
	ExitProcess PROTO Near32 stdcall, dwExitCode:dword  ;capitalization not necessary
	ascint32	PROTO Near32 stdcall, lpStringHolding:dword  ;returns int in EAX
	intasc32	PROTO Near32 stdcall, lpStringToHold:dword, dVal:dword
	intasc32Comma PROTO Near32 stdcall, lpStringToHold:dword, dVal:dword	
	getstring	PROTO Near32 stdcall, lpStringToHold:dword, dNumChars:dword
	putstring	PROTO Near32 stdcall, lpStringToDisplay:dword
	getche		PROTO Near32 stdcall
	getch		PROTO Near32 stdcall
	putch		PROTO Near32 stdcall, bVal:byte	
	
	node struct
		keyValue	dword ?
		recDesc		byte 24 dup(?)
		next		dword ?
	node ENDS
	
	clearString macro stringToClear
	local stLoop							;so it can be used again
	push EAX								;preserve the registers
	push EBX								;preserve the registers
	push ECX
	push EDX
	LEA EBX,stringToClear					;so that ebx has the address
	MOV ECX,26								;so that it will loop 26 times
	stLoop:
		MOV EAX,EBX							;EAX = address of the string
		ADD EAX,ECX							;EAX = address of string + ECX
		MOV DL,0							;since memory to memory can't be done
		MOV [EAX],DL						;Clear this character
	LOOP stLoop								;so the loop will continue
	POP EDX									;restore the registers
	POP ECX
	POP EBX
	ENDM

	clearString24 macro stringToClear
	local stLoop							;so it can be used again
	push EAX								;preserve the registers
	push EBX								;preserve the registers
	push ECX
	push EDX
	LEA EBX,stringToClear					;so that ebx has the address
	MOV ECX,23								;so that it will loop 24 times
	stLoop:
		MOV EAX,EBX							;EAX = address of the string
		ADD EAX,ECX							;EAX = address of string + ECX
		MOV DL," "							;since memory to memory can't be done
		MOV [EAX],DL						;Clear this character
	LOOP stLoop								;so the loop will continue
	MOV EAX,EBX								;EAX = address of the string
	ADD EAX,ECX								;EAX = address of string + ECX
	MOV DL,0								;since memory to memory can't be done
	MOV [EAX],DL							;Clear this character
	POP EDX									;restore the registers
	POP ECX
	POP EBX
	ENDM
	
	.code

Comment#
* Method Name: createPicture
* Method Purpose: To display the datatable
*
* Date created: 11/29/2015
*
* @param lpDatatable, the address of the datatable
* @param number, the number of rows to display
#
createPicture PROC Near32 stdcall uses eax ebx ecx edx, lpDatatable:dword, number:dword
	local strOutput[26]:byte
	ASSUME EBX: ptr node					;makes ebx work as a node
	MOV EBX,[EBP + 8]						;moves the address of the table into EBX
	MOV EDX, number							;gives ecx the number of rows to display
	
	INVOKE putch, 10						;skip down a line
	
	.WHILE EDX > 0							;checks if the next node is at -1 and thus null
		clearString strOutput				;so that the statement will be null terminated
		MOV ECX, 8							;so the loop will go 8 times for the address
		INVOKE intasc32, ADDR strOutput,EBX ;moves the string address of the node into strOutput
		INVOKE putstring,ADDR strOutput		;displays the address as a string
		clearString strOutput				;so that the statement will be null terminated
		INVOKE putch, 9						;tab over
		INVOKE intasc32, ADDR strOutput, [EBX].keyValue		;store the keyValue in decimal in strOutput
		INVOKE putstring,ADDR strOutput						;display the keyValue
		clearString strOutput								;so that the statement will be null terminated
		INVOKE putch, 9										;tab over
		INVOKE putstring,ADDR [EBX].recDesc					;display the record description
		INVOKE intasc32, ADDR strOutput, [EBX].next			;moves the string address of the next node into strOutput
		INVOKE putch, 9						;tab over
		INVOKE putstring,ADDR strOutput		;displays the next address as a string
		clearString strOutput				;so that the statement will be null terminated
		INVOKE putch, 10 					;skips to the next line
		ADD EBX, SIZEOF node				;moves to the next node in the datatable
		DEC EDX								;keep going for the user defined amount of rows
	.ENDW
		INVOKE putch, 10 					;skips to the next line

	RET 									;the return statement
createPicture ENDP	
	
Comment#
* Method Name: delete
* Method Purpose: To delete a node from the list
*
* Date created: 11/29/2015
*
* @param lpStNode, the address of starting node
* @param lpAvail, address of the first node on the list of avaliable nodes
* @param nodeToDelete, the node to be deleted
* @param pred, the preceeding node
* @param succ, the following node
#		
delete PROC Near32 stdcall uses eax ebx ecx edx esi, lpStNode:dword, lpAvail:dword, nodeToDelete:dword,pred:dword, succ:dword
ASSUME EBX: ptr node			;treat ebx as a node
ASSUME EAX: ptr node			;so that eax can be used as a node
ASSUME ECX: ptr node			;treat ecx as a node
MOV ECX,pred					;so that ecx will hold the address of the preceding node
MOV EBX,nodeToDelete			;so that node to delete can be used as a node
MOV EAX,succ					;eax = succ
MOV EDX,[lpStNode]				;edx = stNode
MOV ESI,[lpAvail]				;esi = avail
.IF EDX != -1 && EBX != -1		;check if stNode exists if not do nothing because there are no nodes

MOV [EBX].next,ESI				;move the beginning of the list of available nodes as nodeToDelete's next address
MOV [ESI],EBX					;avail = nodeToDelete

.IF EAX != -1 && ECX != -1		;if it does not come at the beginning or the end
MOV [ECX].next,EAX				;so that pred joins the succ

.ELSEIF EAX == -1				;if it was the last node
MOV EAX,-1						;a register is required in the next operation
MOV [ECX].next,EAX				;change pred to the last node

.ELSEIF ECX == -1				;if it is the first node
MOV [EDX],EAX					;make succ the stNode

.ENDIF

.ENDIF

ASSUME EBX: nothing				;to return the registers
ASSUME EAX: nothing			
ASSUME ECX: nothing				

RET 							;the return statement
delete ENDP
	
Comment#
* Method Name: find
* Method Purpose: To find a specific node in a linked list
*
* Date created: 11/10/2015
*
* @param stNode, the starting node
* @param keyValue, key to search for
* @param lpPred, address of the preceeding node
* @param lpSucc, address of the following node
* @return address of the node that was searched for or a -1 if failed. Returned in eax
#	
find PROC Near32 stdcall uses ebx ecx edx , stNode:dword,keyValue:dword,lpPred:dword,lpSucc:dword
	local nextAddress:sdword, succAddress:dword, predAddress:dword, key:sdword
	ASSUME EBX: ptr node			;treat ebx as a node
	MOV EDX, [EBP + 12]				;cant mem to mem
	MOV key, EDX					;so that ebx holds the value to search for
	MOV EDX, [EBP + 8]				;because you can't do memory to memory
	MOV EBX,EDX						;so that ebx will hold the address of the nodes
	.IF EBX != -1					;make sure the stNode has a value
	MOV EAX, -1						;so that the method will return a -1 if the node is not found
	LEA EDX, [EBP + 20]				;because you can't do memory to memory
	MOV succAddress, EDX			;so that succAddress will hold the address of the successor node
	MOV EDX, EBX					;so that it holds the next address
	MOV [succAddress], EDX			;so that succAddress will return a -1 if no node is found
	LEA EDX, [EBP + 16]				;because you can't do memory to memory
	MOV predAddress, EDX			;so that predAddress will hold the address of the predecessor node
	MOV [predAddress], eax			;so that predAddress will return a -1 if no node is found	 
	
checkKey:
	.WHILE EBX != -1				;checks if there is a next node
	MOV EDX,key						;cant do mem to mem
	.IF [EBX].keyValue == EDX		;checks if the keyValue is found
		JMP found					;if so jump down to found'
	.ENDIF							;end if
	.IF [EBX].keyValue > EDX		;if the keyValue is greater than the method is searching for it should not be in the list since keyValues
									;are in order
		JMP done					;jump down to done since it is not in the list
	.ENDIF
	MOV EDX, EBX					;because you can't do memory to memory
	MOV [predAddress], EDX			;so that predAddress will hold the previous node's address
	MOV ECX, [EBX].next				;move the address of the next node into ecx, because you can't do memory to memory
	MOV [succAddress], ECX			;move the address of the next node into lpSucc	
	MOV EBX,[EBX].next				;go tozX the next node
	.ENDW							;exit the loop
	JMP done						;if the value was never found jump to done
	
found:
	MOV EAX, EBX					;moves the address of the node into eax
	MOV ECX, [EBX].next				;move the address of the next node into ecx, because you can't do memory to memory
	MOV [succAddress], ECX			;move the address of the next node into lpSucc	
	
done:
	MOV EDX, [EBP + 16]				;move the address of pred into edx
	MOV ECX, predAddress			;move predAddress into ecx
	MOV [EDX],ECX					;get the predAddress into the lpPred
	MOV EDX, [EBP + 20]				;move the address of succ into edx
	MOV ECX, succAddress			;move succAddress into ecx
	MOV [EDX],ECX					;get the succAddress into the lpSucc
	.ELSE
	MOV EAX,-1						;if there are no nodes it is not in the list
	MOV EDX, [EBP + 16]				;move the address of pred into edx
	MOV ECX, -1						;ecx = -1
	MOV [EDX],ECX					;pred = -1
	MOV EDX, [EBP + 20]				;edx = address succ
	MOV ECX, -1						;ecx = -1
	MOV [EDX],ECX					;succ = -1
	.ENDIF
	RET								;the leave and return statement
	find ENDP
	
Comment#
* Method Name: getNode
* Method Purpose: To return the next avalible node
*
* Date created: 11/24/2015
*
* @param lpAvail, address of the list of empty nodes to take from
* @return address of the next avalible node or a -1 if failed. Returned in eax
#	
getNode PROC Near32 stdcall uses ebx edx, lpAvail:dword
MOV EDX,lpAvail					;so that EBX holds the address of the start of the nodes to look for
MOV EAX,[EDX]
.IF EAX != -1					;if avail is not -1
ASSUME EAX: ptr node			;treat eax as a node
MOV EBX,[EAX].next				;get the next available address into ebx
MOV [EDX],EBX					;move Avail to the next available address
.ENDIF
RET								;the leave and return statement
getNode ENDP 

Comment#
* Method Name: ini
* Method Purpose: Inializes a data table.
*
* Date created: 11/29/2015
*
* @param lpDatatable, the address of the data table
* @param lpStNode, address of the first node at the beginning of the nodes
* @param lpAvail, address of the next avaliable node
* @param lpPred, address of the previous node
* @param lpSucc, address of the successor node
* @param num, number of rows in the table
#	
ini PROC Near32 stdcall uses eax ebx ecx edx , lpDatatable:dword, lpStNode:dword, lpAvail:dword, lpPred:dword, lpSucc:dword, num:dword
MOV EDX, lpStNode				;moves stNode into EBX
MOV ECX, -1						;cant do mem to mem
MOV [EDX],ECX					;create an empty list
ASSUME EBX: ptr node			;treat ebx as a node
MOV EBX, lpDatatable			;moves the address of the datatable into EBX
MOV ECX,lpAvail					;so ecx hold the address of avail
MOV [ECX],EBX					;moves the address of the datatable into avail
MOV ECX,num						;so that num amount of nodes will be cleared
MOV EAX,0 						;so that eax will hold the amount of bytes to move
	
DEC ECX							;so it will go through the right amount of times
.WHILE ECX != 0					;until ecx is 0
ADD EBX,EAX						;ebx = datatable + number of bytes so far
clearString24 [EBX].recDesc		;empty the recDesc field
MOV [EBX].keyValue, 0 			;make the keyValue a 0 
MOV EDX, EBX					;Make edx equal to the address of the datatable
ADD EDX,SIZEOF Node				;MOV EDX to the next node
MOV [EBX].next,EDX				;change the next node to be the very next node in the table
DEC ECX							;decrement ecx
SUB EBX,EAX						;get ebx back to its original size
ADD EAX,SIZEOF Node				;add the size of a node to eax
.ENDW

ADD EBX,EAX						;so that it is at the last node
clearString24 [EBX].recDesc		;empty the recDesc field
MOV [EBX].keyValue, 0 			;make the keyValue a 0 
MOV [EBX].next,-1				;change the next value for the last node to be a -1
RET								;the leave and return statement
ini ENDP 


Comment#
* Method Name: insert
* Method Purpose: To insert a specific node in a linked list
*
* Date created: 11/24/2015
*
* @param lpStNode, the address of the starting node
* @param nodeToAdd, the node to be added. Should already be an address of an avaliable node
* @param pred, address of the preceding node
* @param succ, address of the following node
#	
insert PROC Near32 stdcall uses eax ebx ecx edx , lpStNode:dword,nodeToAdd:dword,pred:dword,succ:dword
ASSUME EBX: ptr node			;treat ebx as a node
ASSUME EAX: ptr node			;so that eax can be used as a node
MOV ECX,[EBP + 16]				;so that ecx will hold the address of the preceding node
MOV EBX,[EBP + 12]				;so that node to add can be used as a node
MOV EAX,succ					;eax = succ
MOV EDX,[lpStNode]				;can't do memory to memory
.IF EDX != -1					;check if stNode exists

.IF EAX != -1 && ECX != -1		;if it does not come at the beginning
MOV [EAX].next, EBX				;change the pred.next to lead to nodeToAdd
MOV EDX, [EBP + 20]				;can't do memory to memory
MOV [EBX].next,EDX				;nodeToAdd.next = succ

.ELSEIF EAX == -1				;if it was past the last node
MOV EDX, pred					;so that it has what is at the address in stNode
MOV [EDX].next,EBX				;nodeToAdd.next = succ
MOV ECX, -1						;need to use a register
MOV [EBX].next,ECX				;to indicate end of list

.ELSEIF ECX == -1				;if it comes before the list
MOV EAX, nodeToAdd				;can't do the next operation without a register
MOV EBX,[EDX]					;can't do mem to mem
MOV [EAX].next,EBX				;nodeToAdd.next = original stNode
MOV [EDX], EAX					;so that stNode now has the new node
.ENDIF

.ELSE
MOV ECX, nodeToAdd				;can't do the next operation without a register
MOV [EDX], ECX					;so that stNode now has a node
ASSUME EDX: ptr node			;so that eax can be used as a node
MOV [EDX].next,-1				;so that there is no next node
.ENDIF

RET 							;the return statement
insert ENDP

Comment#
* Method Name: traverse
* Method Purpose: To go through all the nodes in a linked list
*
* Date created: 11/10/2015
*
* @param stNode, the starting node]
* @return 0 if worked and -1 if failed in eax
#
traverse proc Near32 stdcall uses ebx ecx edx, stNode:dword
	local strOutput[26]:byte
	ASSUME EBX: ptr node					;makes ebx work as a node
	MOV EBX,[EBP + 8]						;moves the address of the node into EBX
	
	.IF EBX == -1							;checks if the next node is at -1 and thus null
		JE noNodes							;if so jump down
	.ENDIF
	
	.WHILE [EBX].next != -1					;checks if the next node is at -1 and thus null
		clearString strOutput				;so that the statement will be null terminated
		MOV ECX, 8							;so the loop will go 8 times for the address
		INVOKE intasc32, ADDR strOutput,EBX ;moves the string address of the node into strOutput
		INVOKE putstring,ADDR strOutput		;displays the address as a string
		clearString strOutput				;so that the statement will be null terminated
		INVOKE putch, 9						;tab over
		INVOKE intasc32, ADDR strOutput, [EBX].keyValue		;store the keyValue in decimal in strOutput
		INVOKE putstring,ADDR strOutput						;display the keyValue
		clearString strOutput								;so that the statement will be null terminated
		INVOKE putch, 9						;tab over
		INVOKE putstring,ADDR [EBX].recDesc	;display the record description
		INVOKE intasc32, ADDR strOutput, [EBX].next			;moves the string address of the next node into strOutput
		INVOKE putch, 9						;tab over
		INVOKE putstring,ADDR strOutput		;displays the next address as a string
		clearString strOutput				;so that the statement will be null terminated
		INVOKE putch, 10 					;skips to the next line
		MOV EDX, [EBX].next					;moves the node into edx
		MOV EBX, EDX						;moves the next node into ebx
	.ENDW
		clearString strOutput				;so that the statement will be null terminated
		MOV ECX, 8							;so the loop will go 8 times for the address
		INVOKE intasc32, ADDR strOutput,EBX ;moves the string address of the node into strOutput
		INVOKE putstring,ADDR strOutput		;displays the address as a string
		clearString strOutput				;so that the statement will be null terminated
		INVOKE putch, 9						;tab over
		INVOKE intasc32, ADDR strOutput, [EBX].keyValue		;store the keyValue in decimal in strOutput
		INVOKE putstring,ADDR strOutput						;display the keyValue
		clearString strOutput								;so that the statement will be null terminated
		INVOKE putch, 9						;tab over
		INVOKE putstring,ADDR [EBX].recDesc	;display the record description
		INVOKE intasc32, ADDR strOutput, [EBX].next			;moves the string address of the next node into strOutput
		INVOKE putch, 9						;tab over
		INVOKE putstring,ADDR strOutput		;displays the next address as a string
		clearString strOutput				;so that the statement will be null terminated
		INVOKE putch, 10 					;skips to the next line
	JMP lastNode							;so that it won't go to noNodes

noNodes:
	MOV EAX,0								;returns a zero meaning no nodes were found
	JMP finished							;so that it does not go to lastnode
	
lastNode:
	MOV EAX, 1								;returns a one meaning nodes were found
	
finished:
	RET 									;the return statement
traverse ENDP

END