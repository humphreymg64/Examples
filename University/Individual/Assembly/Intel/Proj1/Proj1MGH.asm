;*************************************************************************************
; Program Name:  Proj1MGH.asm
; Programmer:    Matthew Garret Humphrey
; Class:         CSCI 2160
; Date:          September 29, 2015
; Purpose: Add together and display the sum of a user defined number of variables
;
;************************************************************************************* 
	.486
	.model flat
	.stack 100h 
	
	ExitProcess PROTO Near32 stdcall, dwExitCode:dword 
	ascint32	PROTO Near32 stdcall, lpStringHolding:dword 
	intasc32	PROTO Near32 stdcall, lpStringToHold:dword, dVal:dword 
	getstring	PROTO Near32 stdcall, lpStringToHold:dword, dNumChars:dword 
	putstring	PROTO Near32 stdcall, lpStringToDisplay:dword 
	
	.data 
strHeader1	byte	10,13,9,"Name: ",9,"Matthew Humphrey",0
strHeader2	byte	10,13,9,"Class: ",9,"CSCI 2160-001",0
strHeader3	byte	10,13,9,"Date: ",9,"9/28/2015",0
strHeader4	byte	10,13,9,"Lab: ",9,"Project 1",0
strPromptAmt	byte	10,13,9,"How many numbers would you like to enter: ",0 
strPromptNum	byte	10,13,9,"Enter a number: ",0
strAnswer	byte		10,13,9,"The sum of these numbers is ",0
strPressEnter	byte	10,13,9,"Press enter to exit",0
strBlankLn	byte		10,13,0
strDummy	byte	?						;used to be a dummy so that a the program will wait for an enter
strNum		byte	?						;needed to hold the numbers the user will enter
strAmount	byte	?						;needed to hold the amount the user wants to enter
strSum		byte	11 dup(?)				;needed to display the sum
dSum		dword	?						;needed so that the sum can be displayed
dAmount		dword	?						;needed for the looping process, what strAmount converts to
dNums		dword	101 dup(?)				;needed to hold the numbers the user enters 

	.code
_start:
		MOV	ax,0							;instruction used to stop the debugger
		INVOKE putstring, ADDR strHeader1	;tells the user who wrote the program
		INVOKE putstring, ADDR strHeader2	;tells the user the class the program was written for
		INVOKE putstring, ADDR strHeader3	;tells the user when the program was written
		INVOKE putstring, ADDR strHeader4	;tells the user the lab number
		INVOKE putstring, ADDR strBlankLn	;makes the information look more organized
		INVOKE putstring, ADDR strPromptAmt	;asks the user for the amount of numbers to enter
		INVOKE getstring, ADDR strAmount,2	;used to determine how many numbers the user will enter

		INVOKE ascint32, ADDR strAmount		;so that the amount the user entered can be put into ECX for looping
		MOV dAmount, EAX					;so that bAmount has the number in it
		MOV ECX, dAmount					;so that the following loop will go through the user defined amount of times
		MOV EBX, 0							;makes sure that the EBX register is empty
		
stLoopNums:									;used for the user to enter each new number
		MOV strNum, 0						;used to clear strNum every time
		INVOKE putstring, ADDR strPromptNum	;used so that the user knows to enter a number
		INVOKE getstring, ADDR strNum,3						;used to put each number the user enters into strNums
		INVOKE ascint32, ADDR strNum						;so that they can be added together	
		MOV	dAmount[ECX*4], EAX				;so that bAmount has the number in it
		ADD EBX,dAmount[ECX*4]				;used to sum up the numbers
	LOOP stLoopNums							;so that the program will loop
		
		MOV dSum, EBX						;so that the sum will be in dSum
		INVOKE intasc32, ADDR strSum, dSum	;so that the sum is a string which can be displayed
		INVOKE putstring, ADDR strAnswer	;so that the user knows what the amount displayed means
		INVOKE putstring, ADDR strSum		;so that the sum is displayed

		INVOKE putstring, ADDR strPressEnter;so that the user knows to press enter
		INVOKE getstring, ADDR strDummy,0	;so that the program will wait for the user to press enter
		
		INVOKE ExitProcess, 0
		Public _start
		
		end