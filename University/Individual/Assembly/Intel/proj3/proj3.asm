Comment#
**************************************************************************************
 Program Name:  proj3.asm
 Programmer:    Matthew Garret Humphrey
 Class:         CSCI 2160-001
 Date:          October 24, 2015
 Purpose: 		Used to test proj3methods
*************************************************************************************#
	.486
	.model flat
	.stack 100h 
	
	ExitProcess PROTO Near32 stdcall, dwExitCode:dword 
	ascint32	PROTO Near32 stdcall, lpStringHolding:dword 
	intasc32	PROTO Near32 stdcall, lpStringToHold:dword, dVal:dword 
	getstring	PROTO Near32 stdcall, lpStringToHold:dword, dNumChars:dword 
	putstring	PROTO Near32 stdcall, lpStringToDisplay:dword 
	getche		PROTO Near32 stdcall
	getch		PROTO Near32 stdcall
	putch		PROTO Near32 stdcall, bVal:byte	
	extern extractDwords:near, displayArray:near, smallestValue:near, sumUpArray:near, multArrays:near
	
	.data
strHeader1	byte	10,13,9,"Name: ",9,"Matthew Humphrey",0
strHeader2	byte	10,13,9,"Class: ",9,"CSCI 2160-001",0
strHeader3	byte	10,13,9,"Date: ",9,"10/24/2015",0
strHeader4	byte	10,13,9,"Lab: ",9,"Project 3",0
strSizeAR	byte	10,13,"Rows for array A: ",0 
strSizeAC	byte	10,13,"Cols for array A: ",0
strSizeBR	byte	10,13,"Rows for array B: ",0
strSizeBC	byte	10,13,"Cols for array B: ",0
strPrompt	byte	10,13,"If finished, just press enter, otherwise type",0
strErrSize	byte	10,13,"*** Error - Columns for A must = Rows for B ***",0
strEnterA	byte	10,13,"Enter    values for array A seperated by a space (in row order)",0
strEnterB	byte	10,13,"Enter    values for array B seperated by a space (in row order)",0
strArrayA	byte	10,13,"Array A is described below:",0
strArrayB	byte	10,13,"Array B is described below:",0
strSum		byte	10,13,"The sum total for all the elements in this array is ",0
strSmall	byte	10,13,"The smallest value in this array is ",0
strProduct	byte	10,13,"The product of the arrays is described below:",0
strBlnkLine	byte	10,13,0
strTempHold	byte	501 dup(?)						;temporarily holds the value the user enters
dSizeAC		dword	?
dSizeAR		dword	?
dSizeBC		dword	?
dSizeBR		dword	?
dArrayA		dword	20 dup(?)
dArrayB		dword	20 dup(?)
dMulArray	dword	20 dup(?)
dSizeA		dword	?
dSizeB		dword	?
dResult		dword	?
	
	.code
_start:
	mov eax,0								;used to stop the debugger
	
	INVOKE putstring, ADDR strHeader1		;tells the user who wrote the program
	INVOKE putstring, ADDR strHeader2		;tells the user the class the program was written for
	INVOKE putstring, ADDR strHeader3		;tells the user when the program was written
	INVOKE putstring, ADDR strHeader4		;tells the user the lab number
	
	JMP getSizes							;to skip sizeError
	
sizeError:
	INVOKE putstring, ADDR strErrSize		;tell user what went wrong
	JMP getSizes							;go back and try again
	
getSizes:
	INVOKE putstring, ADDR strSizeAC		;prompt for a cols
	INVOKE getstring,ADDR strTempHold, 10	;get a cols
	INVOKE ascint32,ADDR strTempHold		;turn to ascii
	MOV dSizeAC, EAX						;store a cols
	INVOKE putstring, ADDR strSizeAR
	INVOKE getstring,ADDR strTempHold, 10
	INVOKE ascint32,ADDR strTempHold
	MOV dSizeAR, EAX
	INVOKE putstring, ADDR strSizeBC
	INVOKE getstring,ADDR strTempHold, 10
	INVOKE ascint32,ADDR strTempHold
	MOV dSizeBC, EAX
	INVOKE putstring, ADDR strSizeBR
	INVOKE getstring,ADDR strTempHold, 10
	INVOKE ascint32,ADDR strTempHold
	MOV dSizeBR, EAX
	CMP dSizeAC, EAX
	JNE sizeError
	IMUL dSizeBC
	MOV dSizeB,EAX
	MOV EAX,dSizeAC
	IMUL dSizeAR
	MOV dSizeA,EAX

addNumbersA:	
	INVOKE intasc32, ADDR strTempHold,dSizeA
	MOV AL, strTempHold
	MOV strEnterA[8], AL
	MOV AL, strTempHold[1]
	CMP AL,0
	JNE addSecondA
	
addNumbersB:	
	INVOKE intasc32,ADDR strTempHold,dSizeB
	MOV AL, strTempHold
	MOV strEnterB[8], AL
	MOV AL, strTempHold[1]
	CMP AL,0
	JNE addSecondA
	JMP displayMatrix
	
addSecondA:
	MOV strEnterA[9], AL
	JMP addNumbersB

addSecondB:
	MOV strEnterB[9],AL
	
displayMatrix:	
	INVOKE putstring,ADDR strEnterA
	INVOKE putstring,ADDR strBlnkLine
	INVOKE getstring,ADDR strTempHold, 50
	
	PUSH OFFSET strTempHold
	PUSH OFFSET dArrayA
	CALL extractDwords
	ADD ESP, 8
	
	INVOKE putstring,ADDR strEnterB
	INVOKE putstring,ADDR strBlnkLine
	INVOKE getstring,ADDR strTempHold, 50
	
	PUSH OFFSET strTempHold
	PUSH OFFSET dArrayB
	CALL extractDwords
	ADD ESP, 8
	
	INVOKE putstring,ADDR strArrayA
	INVOKE putstring,ADDR strBlnkLine
	PUSH OFFSET strTempHold
	PUSH dSizeAC
	PUSH dSizeAR
	PUSH OFFSET dArrayA
	CALL displayArray
	ADD ESP,16
	
	PUSH dSizeAC
	PUSH dSizeAR
	PUSH OFFSET dArrayA
	CALL sumUpArray
	ADD ESP, 12
	MOV dResult, EAX
	INVOKE putstring,ADDR strSum
	INVOKE intasc32,ADDR strTempHold,dResult
	INVOKE putstring,ADDR strTempHold
	
	PUSH dSizeAC
	PUSH dSizeAR
	PUSH OFFSET dArrayA
	CALL smallestValue
	ADD ESP,12
	MOV dResult, EAX
	
	INVOKE intasc32,ADDR strTempHold,dResult
	INVOKE putstring,ADDR strBlnkLine
	INVOKE putstring,ADDR strSmall
	INVOKE putstring,ADDR strTempHold
	
	INVOKE putstring,ADDR strBlnkLine
	INVOKE putstring,ADDR strArrayB
	INVOKE putstring,ADDR strBlnkLine
	PUSH OFFSET strTempHold
	PUSH dSizeBC
	PUSH dSizeBR
	PUSH OFFSET dArrayB
	CALL displayArray
	ADD ESP,16

	PUSH dSizeBC
	PUSH dSizeBR
	PUSH OFFSET dArrayB
	CALL sumUpArray
	ADD ESP, 12
	MOV dResult, EAX

	INVOKE putstring,ADDR strSum
	INVOKE intasc32,ADDR strTempHold,dResult
	INVOKE putstring,ADDR strTempHold
	PUSH dSizeBC
	PUSH dSizeBR
	PUSH OFFSET dArrayB
	CALL smallestValue
	ADD ESP,12
	MOV dResult, EAX

	INVOKE intasc32,ADDR strTempHold,dResult
	INVOKE putstring,ADDR strBlnkLine
	INVOKE putstring,ADDR strSmall
	INVOKE putstring,ADDR strTempHold
	INVOKE putstring,ADDR strBlnkLine
	
	INVOKE putstring,ADDR strProduct
	INVOKE putstring,ADDR strBlnkLine
	PUSH OFFSET dMulArray
	PUSH dSizeBC
	PUSH OFFSET dArrayB
	PUSH dSizeAC
	PUSH dSizeAR
	PUSH OFFSET dArrayA
	CALL multArrays
	ADD ESP,24
	
	PUSH OFFSET strTempHold
	PUSH dSizeBC
	PUSH dSizeAR
	PUSH OFFSET dMulArray
	call displayArray
	ADD ESP,16
	
	INVOKE putstring,ADDR strPrompt
	INVOKE getstring,ADDR strTempHold, 5
	CMP strTempHold,0
	JE finished
	JMP _start
	
finished:
	INVOKE ExitProcess,0
	PUBLIC _start	
	END