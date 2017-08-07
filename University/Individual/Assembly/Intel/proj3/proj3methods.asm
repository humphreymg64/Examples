Comment#
**************************************************************************************
 Program Name:  proj3methods.asm
 Programmer:    Matthew Garret Humphrey
 Class:         CSCI 2160-001
 Date:          October 24, 2015
 Purpose: 		Methods for matrix manipulation
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
	INC ESI									;esi ++
	JMP findNums							;jump back
	
addNum:
	cmp ECX,0								;check if their last item was a value
	JE nextChar								;if not jump to next
	MOV AL,0								;al = 0
	MOV [ESI],AL							;[esi] = 0
	MOV EAX, ESI							;eax = esi
	SUB EAX, ECX							;EAX = ESI - ECX
	INVOKE ascint32,ADDR [EAX]				;change the last string of numbers to a dword
	MOV [EBX],EAX							;[ebx] = eax
	ADD EBX,4								;ebx += 4
	MOV ECX, 0								;to clear the ecx register
	INC ESI									;esi++
	JMP findNums							;jump back up
	
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

	MOV EAX,[EBP + 16]						;eax = cols
	MOV EDX,[EBP + 12]						;edx = rows
	IMUL EDX								;edx:eax = cols * rows
	MOV EDX,12								;edx = 12; length of dword max value in chars
	IMUL EDX								;edx:eax = cols * rows * 12
	MOV ECX,EAX								;ecx = eax
	
stClear:
	MOV AL," "								;al = " "
	MOV [ESI],AL							;clear out each space in esi
	INC ESI									;next space
	LOOP stClear							;loop till all spaces

checkOnes:									;so that no extras are done
	MOV EAX,[EBP + 16]						;eax = cols
	MOV EDX,[EBP + 12]						;edx = rows
	CMP EDX,1								;check if edx = 1
	JE colsOne								;if so jump to colsOne
	CMP EAX,1								;check if cols = 1
	JE rowsOne								;if so jump to rowsOne
	JMP noOnes								;if neither is a one jump down
	
colsOne:
	MOV ECX, EAX							;ecx = cols
	JMP setOthers							;jump down
	
rowsOne:
	MOV ECX,EDX								;ecx = rows
	JMP setOthers							;jump down
	
noOnes:
	IMUL EDX								;edx:eax = rows * cols
	MOV ECX,EAX								;ecx = eax
	
setOthers:	
	MOV ESI, [EBP + 20]						;so that esi will hold the address of the string to hold	
	MOV EDI, [EBP + 12]						;so that ecx will hold the amount of rows and loop that many times
	MOV EDX, [EBP + 16]						;so that edx will hold the amount of columns 
	
addNumber:
	INVOKE intasc32,ADDR [ESI],[EBX]		;converts the dwords to a string
	ADD EBX,4								;move ebx to the next word
	DEC ECX									;one num down
	DEC EDX									;one col back
	
checkEnd:
	MOV EAX,0 								;eax = 0
	CMP ECX,EAX								;check if finished
	JE finished								;if so jump to finished

checkZeros:	
	MOV AL,0								;al = 0
	CMP [ESI],AL							;check for zero chars from intasc
	JE makeTab								;if found turn to tab
	INC ESI									;next char
	JMP checkEnd							;jump back to first check
	
makeTab:
	MOV AL,9								;al = 9 = "\t"
	MOV [ESI],AL							;turn [est] to tab
	
checkEndRow:
	MOV EAX,0								;eax = 0 
	CMP EDX,EAX								;check if end of a row
	JE nextLine								;if so insert new line char
	INC ESI									;next char
	JMP addNumber							;add next num
	
nextLine:
	MOV AL,10								;put new line char in al
	MOV [ESI],AL							;change [esi] into new line char
	MOV EDX, [EBP + 16]						;so that edx will hold the amount of columns 
	INC ESI									;next char
	JMP addNumber							;add next num
	
finished:
	INC ESI									;next char
	MOV AL,0								;al = 0
	MOV [ESI],AL							;change last char to 0
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
	ADD EBX, 4								;mov to the next number
	DEC ECX									;so that ecx will loop the right amount of times
	
	
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
	MOV ECX, [EBP + 12]						;ECX = rows in b
	IMUL ECX								;EDX:EAX = rows in a * columns in b
	MOV ECX, EAX							;ECX = Rows in a * Columns in b
	MOV EDX,4								;EDX = 4
	IMUL EDX								;EDX:EAX = rows in a * columns in b * 4
	ADD ESI, EAX							;ESI = Address of b + EAX

goToEndA:
	MOV EAX, [EBP + 12]						;EAX = Columns in a
	MOV EDX, [EBP + 16]						;EDX = Rows in a
	IMUL EDX								;EDX:EAX = Columns in a * Rows in a
	MOV EDX,4								;EDX = 4
	IMUL EDX								;EDX:EAX = Columns in a * Rows in a * 4
	ADD EBX, EAX							;EBX = Address of a + EAX

setNew:
	MOV EDI, [EBP + 28]						;EDI = Address of new array		
	
stLoop:
	MOV EAX, 4 								;EAX = 4 
	IMUL ECX								;EDX:EAX = 4 * ECX 
	ADD EDI, EAX							;EDI = Address of new array + EAX
	
	MOV EAX, [ESI]							;EAX = ESI
	MOV EDX,[EBX]							;EDX = last number in a
	IMUL EDX								;EDX:EAX = ESI * EBX
	MOV [EDI],EAX							;Last value of the new array is now ESI * EBX

	MOV EAX,ECX								;eax = ecx
	ADD ECX,[EBP + 16]						;ecx += rows in a
	DEC ECX									;ecx--; since one multiplication has already been done

	CMP ECX,EDX								;check if out of cols
	JNE addNums								;if so a next part
	LOOP stLoop								;loop whole thing
	JMP finished							;jump finished
	
stCheckForAdd:
	CMP ECX,EDX								;check if out of cols
	JNE addNums								;if so a next part
	LOOP stLoop								;loop whole thing
addNums:	
	MOV EAX,4 								;EAX = 4
	IMUL byte ptr [EBP + 24]				;EDX:EAX = 4 * columns in b
	ADD ESI, EAX							;ESI -= 4 * columns in b; moved back one row
	ADD EBX, 4								;EBX -= 4 ; moved back one column
	MOV EAX, [ESI]							;EAX = ESI
	IMUL byte ptr [EBX]						;EDX:EAX = ESI * EBX
	ADD [EDI],EAX							;Last value of the new array is now ESI * EBX + another row in each array
	
	LOOP stCheckForAdd						;loop back to checking for more additions

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
	
