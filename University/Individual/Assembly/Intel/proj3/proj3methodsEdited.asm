Comment#
**************************************************************************************
 Program Name:  proj3methods.asm
 Programmer:    Matthew Garret Humphrey
 Class:         CSCI 2160-001
 Date:          October 24, 2015
 Purpose: 		
*************************************************************************************#
	.486
	.model flat 
	
	ExitProcess PROTO Near32 stdcall, dwExitCode:dword 
	ascint32	PROTO Near32 stdcall, lpStringHolding:dword 
	intasc32	PROTO Near32 stdcall, lpStringToHold:dword, dVal:dword 
	getstring	PROTO Near32 stdcall, lpStringToHold:dword, dNumChars:dword 
	putstring	PROTO Near32 stdcall, lpStringToDisplay:dword 
	getche		PROTO Near32 stdcall
	getch		PROTO Near32 stdcall
	putch		PROTO Near32 stdcall, bVal:byte	
	
	.code

extractDwords PROC
	PUSH EBP								;to preserve the original EBP
	MOV EBP,ESP								;set up for the new stack frame
	PUSH EAX								;to preserve the registers that will be used
	PUSH EBX								
	PUSH ECX
	PUSH ESI
	
	MOV EBX, [EBP + 8]						;so ebx will hold the address of the dwords
	MOV ESI, [EBP + 12]						;so esi will hold the address of the string
	MOV ECX, 0								;to clear the ecx register
	
findNums:
	MOV AL, [ESI]							;because memory can't be compared to memory
	CMP AL, 0								;tests if the current character is an end of line character
	JE endOfLine							;if so skip to the end of the method
	MOV AL, [ESI]							;because memory can't be compared to memory
	CMP AL, ' '								;tests if the current character is a blank space
	JE addNum								;if so skip to the next character in the string
	MOV AL, [ESI]							;because memory can't be compared to memory
	CMP AL, ','								;tests if the current character is a ,
	JE addNum								;if so skip to the next character in the string
	MOV AL, [ESI]							;because memory can't be compared to memory
	CMP AL, ';'								;tests if the current character is a ;
	JE addNum								;if so skip to the next character in the string
	MOV AL, [ESI]							;because memory can't be compared to memory
	CMP AL, ':'								;tests if the current character is a :
	JE addNum								;if so skip to the next character in the string
	MOV AL, [ESI]							;because memory can't be compared to memory
	CMP AL, 9								;tests if the current character is a tab character
	JE addNum								;if so skip to the next character in the string
	INC ECX									;add one to the length of the current word
	
nextChar:
	INC ESI
	JMP findNums
	
addNum:
	cmp ECX,0
	JE nextChar
	MOV AL,0
	MOV [ESI],AL
	MOV EAX, ESI
	SUB EAX, ECX							;EAX = ESI - ECX
	INVOKE ascint32,ADDR [EAX]				;change the last string of numbers to a dword
	MOV [EBX],EAX
	ADD EBX,4	
	MOV ECX, 0								;to clear the ecx register
	INC ESI
	JMP findNums
	
;nextChar:
;	CMP ECX,0								;test if the length of the last word was zero
;	JNE addNum								;if not jump to a part that adds a number to the dword array
;	MOV AL,0								;AL = 0
;	MOV [ESI], AL							;changes the current character to an end of line character so that the ascint32 will read it as a word
;	INC ESI									;so that the next character will be tested
;	JMP findNums							;jump back up to the part that tests for whitespace
;	
;addNum:
;	SUB ESI, ECX							;ESI = ESI - ECX
;	INVOKE ascint32,ADDR [ESI]				;change the last string of numbers to a dword
;	MOV [EBX], EAX							;move the new dword into the address stored in ebx
;	ADD EBX, 4								;move to the next dword in the array whose address ebx holds
;	MOV ECX, 0								;change the length of the last word back to zero
;	INC ESI									;move to the next character in the string whose address is held by esi
;	JMP findNums							;jump back to the part that test for whitespace
;	
endOfLine:
	CMP ECX,0								;test if the length of the last word was zero
	JNE addNum								;if not jump to a part that adds a number to the dword array
	POP ESI									;restore the registers
	POP ECX									;
	POP EBX									;
	POP EAX									;
	POP EBP									;
	RET										;the leave and return statement
	extractDwords ENDP
	
displayArray PROC
	PUSH EBP								;to preserve the original EBP
	MOV EBP,ESP								;set up for the new stack frame
	PUSH EAX								;to preserve the registers that will be used
	PUSH EBX
	PUSH ECX
	PUSH EDX
	PUSH EDI
	PUSH ESI
	
	MOV EBX, [EBP + 8]						;so that ebx will hold the address of the dwords
	MOV ESI, [EBP + 20]						;so that esi will hold the address of the string to hold

	MOV EAX,[EBP + 16]
	MOV EDX,[EBP + 12]
	IMUL EDX 
	MOV EDX,12
	IMUL EDX
	MOV ECX,EAX
	
stClear:
	MOV AL," "
	MOV [ESI],AL
	INC ESI
	LOOP stClear

checkOnes:
	MOV EDX,[EBP + 16]
	MOV EAX,[EBP + 12]
	CMP EDX,1
	JE colsOne
	CMP EAX,1
	JE rowsOne
	JMP noOnes
	
colsOne:
	MOV ECX, EAX
	JMP setOthers
	
rowsOne:
	MOV ECX,EDX
	JMP setOthers
	
noOnes:
	MOV ECX,EAX
	MOV EAX,EDX
	IMUL EDX
	ADD ECX,EAX
	
setOthers:	
	MOV ESI, [EBP + 20]						;so that esi will hold the address of the string to hold	
	MOV EDI, [EBP + 12]						;so that ecx will hold the amount of rows and loop that many times
	MOV EDX, [EBP + 16]						;so that edx will hold the amount of columns 
	
addNumber:
	INVOKE intasc32,ADDR [ESI],[EBX]		;converts the dwords to a string
	ADD EBX,4								;move ebx to the next word
	DEC ECX
	DEC EDX
	
checkEnd:
	MOV EAX,0 
	CMP ECX,EAX
	JE finished

checkZeros:	
	MOV AL,0
	CMP [ESI],AL
	JE makeTab
	INC ESI
	JMP checkEnd
	
makeTab:
	MOV AL,9
	MOV [ESI],AL
	
checkEndRow:
	MOV EAX,0
	CMP EDX,EAX
	JE nextLine	
	INC ESI
	JMP addNumber
	
nextLine:
	MOV AL,10
	MOV [ESI],AL
	MOV EDX, [EBP + 16]						;so that edx will hold the amount of columns 
	INC ESI
	JMP addNumber
	
;stRowLoop:
;	INVOKE intasc32,ADDR [ESI],[EBX]		;converts the dwords to a string
;	ADD EBX,4								;move ebx to the next word
;	ADD ESI,12								;move esi to the next space for a word
;	INC ESI									;so that it will be on the next character
;	DEC EDX
;	CMP EDX,0
;	JE stRowLoop
;	MOV EDX, [EBP + 16]
;	
;stColLoop:
;	MOV EAX, 10								;EAX = 10; to not do memory to memory
;	INC ESI
;	MOV [ESI],EAX							;skip down a line
;	LOOP stRowLoop							;loop the whole thing
;	
;	MOV ESI, [EBP + 20]						;so that esi will hold the address of the string to hold	
;	MOV EAX,[EBP + 16]
;	MOV EDX,[EBP + 12]
;	IMUL EDX
;	MOV EDX,12
;	IMUL EDX
;	MOV ECX,EAX
;	
;stClearEnds:
;	MOV AL,0
;	CMP [ESI],AL
;	JNE next
;	MOV AL," "
;	MOV [ESI],AL
;	INC ESI
;
;next:
;	INC ESI
;	LOOP stClearEnds
	
finished:
	INC ESI
	MOV AL,0
	MOV [ESI],AL
	INVOKE putstring, [EBP + 20]			;display the array
	POP ESI									;put the registers back
	POP EDI
	POP EDX
	POP ECX
	POP EBX
	POP EAX
	POP EBP
	RET										;the leave and return statement
	displayArray ENDP
	
sumUpArray PROC
	PUSH EBP								;to preserve the original EBP
	MOV EBP,ESP								;set up for the new stack frame
	PUSH EBX								;to preserve the registers that will be used
	PUSH ECX
	PUSH EDX
	
	MOV EBX, [EBP + 12]						;EBX = rows
	MOV EAX, [EBP + 16]						;EAX = columns
	IMUL EBX								;EDX:EAX = EBX * EAX
	MOV ECX, EAX							;ECX = EBX*EAX
	MOV EBX, [EBP + 8]						;so that ebx holds the address 
	MOV EAX, [EBX]							;so that eax holds the answer
	ADD EBX, 4								;mov to the next number
	DEC ECX									;so that ecx will loop the right amount of times
	
stAddUp:
	ADD EAX,[EBX]							;add the next dword
	ADD EBX,4								;move to the next dword
	LOOP stAddUp							;loop until all dwords summed up
	
	POP EDX									;put the registers back
	POP EBX
	POP ECX
	POP EBP
	RET										;the leave and return statement
	sumUpArray ENDP
	
smallestValue PROC
	PUSH EBP								;to preserve the original EBP
	MOV EBP,ESP								;set up for the new stack frame
	PUSH EBX								;to preserve the registers that will be used
	PUSH ECX
	PUSH EDX
	PUSH ESI
	
	MOV EBX, [EBP + 12]						;EBX = rows
	MOV EAX, [EBP + 16]						;EAX = columns
	IMUL EBX								;EDX:EAX = EBX * EAX
	MOV ECX, EAX							;ECX = EBX*EAX
	MOV EBX, [EBP + 8]						;so that ebx holds the address 
	MOV EAX, [EBX]							;so that eax holds the answer
	MOV ESI, [EBX + 4]						;so that esi will hold the next value
;	DEC ECX									;so that ecx will loop the right amount of times
	
stComparing:
	CMP EAX,ESI								;test if esi is greater than eax
	JG smaller								;if so jump down to a part that moves esi int eax
	ADD EBX,4								;move to the next dword
	MOV ESI,[EBX]							;so that esi will hold the next value
	LOOP stComparing						;loop until completely compared
	JMP done								;so that the loop will not be done twice
	
smaller:
	MOV EAX, ESI							;move esi into eax
	LOOP stComparing						;loop the loop
	
done:	
	POP ESI									;put the registers back
	POP EDX									
	POP ECX
	POP EBX
	POP EBP
	RET										;the leave and return statement
	smallestValue ENDP
	
multArrays PROC
	PUSH EBP								;to preserve the original EBP
	MOV EBP,ESP								;set up for the new stack frame
	PUSH EAX								;to preserve the registers that will be used
	PUSH EBX
	PUSH ECX
	PUSH EDI
	PUSH EDX
	PUSH ESI

	MOV ESI, [EBP + 20]						;ESI = Address of b
	MOV EBX, [EBP + 8]						;EBX = Address of a
	
goToEndB:	
	MOV EAX, [EBP + 24]						;EAX = Columns in b
	MOV ECX, [EBP + 16]						;ECX = rows in a
	IMUL ECX								;EDX:EAX = rows in a * columns in b
	MOV ECX, EAX							;ECX = Rows in a * Columns in b
	MOV EDX,4								;EDX = 4
	IMUL EDX								;EDX:EAX = rows in a * columns in b * 4
	ADD ESI, EAX							;ESI = Address of b + EAX
	MOV EAX, [EBP + 24]						;
	CMP EAX, 1								;
	JE equalOneB1							;

checkSecondOneB:	
	MOV EAX, [EBP + 16]						;
	CMP EAX,1 								;
	JE equalOneB2							;
	JMP goToEndA							;
	
equalOneB1:
	SUB ESI,4 								;
	JMP checkSecondOneB						;
	
equalOneB2:
	SUB ESI,4 								;

goToEndA:
	MOV EAX, [EBP + 12]						;EAX = Columns in a
	MOV EDX, [EBP + 16]						;EDX = Rows in a
	IMUL EDX								;EDX:EAX = Columns in a * Rows in a
	MOV EDX,4								;EDX = 4
	IMUL EDX								;EDX:EAX = Columns in a * Rows in a * 4
	ADD EBX, EAX							;EBX = Address of a + EAX
	MOV EAX, [EBP + 12]						;
	CMP EAX, 1								;
	JE equalOneA1							;

checkSecondOneA:	
	MOV EAX, [EBP + 16]						;
	CMP EAX,1 								;
	JE equalOneA2							;
	JMP setNew								;
	
equalOneA1:
	SUB EBX,4 								;
	JMP checkSecondOneA						;
	
equalOneA2:
	SUB EBX,4 								;

setNew:
	MOV EDI, [EBP + 28]						;EDI = Address of new array		
	MOV AL, [EBP + 24]						;EAX = Columns in b
	MOV CL, [EBP + 16]						;ECX = rows in a
	IMUL CL									;EDX:EAX = rows in a * columns in b
	MOV CL, AL								;ECX = Rows in a * Columns in b
	MOV CH, [EBP + 16]						;
	MOV EAX,0								;
	
stLoop:
	MOV AL, 4 								;EAX = 4 
	IMUL CL									;EDX:EAX = 4 * ECX 
	ADD EDI, EAX							;EDI = Address of new array + EAX
	
	MOV EAX, [ESI]							;EAX = ESI
	MOV EDX,[EBX]							;EDX = last number in a
	IMUL EDX								;EDX:EAX = ESI * EBX
	MOV [EDI],EAX							;Last value of the new array is now ESI * EBX

	MOV AL,CL								;
	ADD CL,[EBP + 16]						;
	DEC CL									;ecx--; since one multiplication has already been done

	DEC CH									;
	CMP CL,CH								;
	JNE addNums								;
	DEC CL									;
	CMP CL,0								;
	JNE stLoop								;
	JMP finished							;
	
stCheckForAdd:
	CMP CL,CH								;
	JNE addNums								;
	DEC CL									;
	CMP CL,0								;
	JNE stLoop								;
	
addNums:	
	MOV EAX,4 								;EAX = 4
	IMUL dword ptr [EBP + 24]				;EDX:EAX = 4 * columns in b
	ADD ESI, EAX							;ESI -= 4 * columns in b; moved back one row
	ADD EBX, 4								;EBX -= 4 ; moved back one column
	MOV EAX, [ESI]							;EAX = ESI
	IMUL dword ptr [EBX]					;EDX:EAX = ESI * EBX
	ADD [EDI],EAX							;Last value of the new array is now ESI * EBX + another row in each array
	
	DEC CL									;
	CMP CL,0								;
	JNE stCheckForAdd						;
	
finished:

	POP ESI									;fix the registers
	POP EDX
	POP EDI
	POP ECX
	POP EBX
	POP EAX
	POP EBP
	
	RET										;the leave and return statement
	multArrays ENDP
	
	END