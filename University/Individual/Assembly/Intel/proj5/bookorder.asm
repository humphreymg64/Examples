COMMENT#
************************************************************************************
Name:		Matthew Humphrey
Program:  	BookOrder.asm
Class:		CSCI 2160-001
Date:		12/1/2015
Purpose:	To convert the java statments in the BookOrder uml to assembly

************************************************************************************  
#
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
	memoryallocBailey PROTO stdcall, dSize:dword
	strLength 	PROTO Near32 stdcall, string:dword
	strcpy 		PROTO Near32 stdcall, strSource:dword, strDest:dword
	
	
BookOrder struct
	author dword ?
	strTitle dword ?
	quantity dword ?
	costPerBook dword ?
	weight dword ?
BookOrder ends
	
	.code

;* Method Name: BookOrder_1
;* Method Purpose: An empty constructor
;*
;* Date created: 12/1/2015
;*
;* @return address of a memory allocation for a BookOrder
BookOrder_1 PROC Near32 stdcall
MOV EAX, SIZE BookOrder			;so that memory can be allocated for a bookorder
INVOKE memoryallocBailey, EAX	;gets the address of the memory needed into eax
RET
BookOrder_1 ENDP

;* Method Name: BookOrder_2
;* Method Purpose: A parametrised constructor
;*
;* Date created: 12/1/2015
;*
;* @param author, address of string for the author
;* @param title, address of string for the title
;* @param quantity, amount of items ordered 
;*
;* @return address of a memory allocation for a BookOrder	
BookOrder_2 PROC Near32 stdcall uses ebx, author:dword, strTitle:dword, quantity:dword
MOV EAX, SIZE bookorder			;so that memory can be allocated for a bookorder
INVOKE memoryallocBailey, EAX	;gets the address of the memory needed into eax
MOV EBX,EAX						;since eax will be overwritten
ASSUME EBX:ptr bookorder		;so that the parameters at the address stored in ebx can be changed
INVOKE strLength, author		;get the length of the string into eax
INVOKE memoryallocBailey, eax	;get an address for the author
MOV [EBX].author,eax			;so that the author field will have the address of usable memory
INVOKE strcpy, [EBX].author,author		;copies the information in author into the bookorder
INVOKE strLength, strTitle				;get the length of the string into eax
INVOKE memoryallocBailey, eax			;get an address for the author
MOV [EBX].strTitle,eax					;so that the author field will have the address of usable memory
INVOKE strcpy, [EBX].strTitle,strTitle	;copies the information in author into the bookorder
MOV EAX,quantity						;since memory to memory can't be done
MOV [EBX].quantity,EAX					;bookorder.quantity = quantity
MOV EAX,EBX						;so that it returns the address of the the bookorder
ASSUME EBX:nothing				;to return the system to assuming nothing
RET
BookOrder_2 ENDP

;* Method Name: BookOrder_3
;* Method Purpose: A copy constructor
;*
;* Date created: 12/1/2015
;*
;* @param b, the bookorder to copy
;*
;* @return address of a memory allocation for a copied BookOrder	
BookOrder_3 PROC Near32 stdcall uses ebx edx, b:dword
MOV EAX, SIZE BookOrder			;so that memory can be allocated for a bookorder
INVOKE memoryallocBailey, EAX	;gets the address of the memory needed into eax
MOV EBX,EAX						;since eax will be overwritten
MOV EDX,b						;so that EDX holds the old bookorder's address
ASSUME EBX: ptr BookOrder		;so that the parameters at the address stored in ebx can be changed
ASSUME EDX: ptr BookOrder		;so that the parameters at the address stored in edx can be changed
INVOKE strLength, [EDX].author	;get the length of the string into eax
INVOKE memoryallocBailey, eax	;get an address for the author
MOV [EBX].author,eax			;so that the author field will have the address of usable memory
INVOKE strcpy, [EBX].author, [EDX].author		;copies the information in author into the bookorder
INVOKE strLength, [EDX].strTitle				;get the length of the string into eax
INVOKE memoryallocBailey, eax					;get an address for the author
MOV [EBX].strTitle,eax							;so that the author field will have the address of usable memory
INVOKE strcpy, [EBX].strTitle, [EDX].strTitle	;copies the information in author into the bookorder
MOV EAX,[EDX].quantity							;since memory to memory can't be done
MOV [EBX].quantity,EAX				;bookorder.quantity = quantity
MOV EAX,EBX						;so that it returns the address of the the bookorder
ASSUME EBX:nothing				;to return the system to assuming nothing
ASSUME EDX:nothing
RET
BookOrder_3 ENDP

;* Method Name: setAuthor
;* Method Purpose: A setter for the author attribute
;*
;* Date created: 12/1/2015
;*
;* @param ths, the object's address
;* @param author, the author to add
;*	
setAuthor PROC Near32 stdcall uses eax ebx , ths:dword, author:dword
ASSUME EBX: ptr BookOrder		;so that the parameters at the address stored in ebx can be changed
MOV EAX,author					;EAX = address of the author to set
MOV EBX,ths						;EBX = address of the 
INVOKE memoryallocBailey, EAX	;get an address for the author
MOV [EBX].author,eax				;so that the author field will have the address of usable memory
INVOKE strcpy, [EBX].author, author	;copies the information in author into the bookorder
ASSUME EBX:nothing					;to return the system to assuming nothing
RET
setAuthor ENDP

;* Method Name: setTitle
;* Method Purpose: A setter for the title attribute
;*
;* Date created: 12/3/2015
;*
;* @param ths, the object's address
;* @param strTitle, the title to add
;*	
setTitle PROC Near32 stdcall uses eax ebx, ths:dword, strTitle:dword
ASSUME EBX:ptr BookOrder			;so that the parameters at the address stored in ebx can be changed
MOV EAX,strTitle					;EAX = address of the title to set
MOV EBX,ths							;EBX = address of the 
INVOKE memoryallocBailey, EAX		;get an address for the title
MOV [EBX].strTitle,eax					;so that the title field will have the address of usable memory
INVOKE strcpy,[EBX].strTitle,strTitle	;copies the information in title into the bookorder
ASSUME EBX:nothing						;to return the system to assuming nothing
RET
setTitle ENDP

;* Method Name: setQuantity
;* Method Purpose: A setter for the quantity attribute
;*
;* Date created: 12/3/2015
;*
;* @param ths, the object's address
;* @param quantity, the quantity to add
;*	
setQuantity PROC Near32 stdcall uses eax ebx, ths:dword, quantity:dword
ASSUME EBX: ptr BookOrder		;so that the parameters at the address stored in ebx can be changed
MOV EAX,quantity				;EAX = address of the quantity to set
MOV EBX,ths						;EBX = address of the object
MOV [EBX].quantity,EAX			;copies the information in quantity into the bookorder
ASSUME EBX:nothing				;to return the system to assuming nothing
RET
setQuantity ENDP

;* Method Name: setCostPerBook
;* Method Purpose: A setter for the CostPerBook attribute
;*
;* Date created: 12/4/2015
;*
;* @param ths, the object's address
;* @param cost, the CostPerBook to add
;*	
setCostPerBook PROC Near32 stdcall uses eax ebx, ths:dword, cost:dword
ASSUME EBX: ptr BookOrder		;so that the parameters at the address stored in ebx can be changed
MOV EAX,cost					;EAX = address of the CostPerBook to set
MOV EBX,ths						;EBX = address of the object
MOV [EBX].costPerBook,EAX		;copies the information in CostPerBook into the bookorder
ASSUME EBX:nothing				;to return the system to assuming nothing
RET
setCostPerBook ENDP

;* Method Name: setWeight
;* Method Purpose: A setter for the weight attribute
;*
;* Date created: 12/3/2015
;*
;* @param ths, the object's address
;* @param weight, the weight to add
;*	
setWeight PROC Near32 stdcall uses eax ebx, ths:dword, weight:dword
ASSUME EBX: ptr BookOrder		;so that the parameters at the address stored in ebx can be changed
MOV EAX,weight					;EAX = address of the weight to set
MOV EBX,ths						;EBX = address of the object
MOV [EBX].weight,EAX			;copies the information in weight into the bookorder
ASSUME EBX:nothing				;to return the system to assuming nothing
RET
setWeight ENDP

;* Method Name: getAuthor
;* Method Purpose: A getter for the author attribute
;*
;* Date created: 12/4/2015
;*
;* @param ths, the object's address
;*	
;* @return address of the author string
getAuthor PROC Near32 stdcall uses ebx, ths:dword
ASSUME EBX: ptr bookorder		;so that the parameters at the address stored in ebx can be changed
MOV EBX,ths						;EBX = address of the object
MOV EAX,[EBX].author			;copies the information in author into the bookorder
ASSUME EBX:nothing				;to return the system to assuming nothing
RET
getAuthor ENDP

;* Method Name: getTitle
;* Method Purpose: A getter for the Title attribute
;*
;* Date created: 12/4/2015
;*
;* @param ths, the object's address
;*	
;* @return address of the title string
getTitle PROC Near32 stdcall uses ebx, ths:dword
ASSUME EBX: ptr bookorder		;so that the parameters at the address stored in ebx can be changed
MOV EBX,ths						;EBX = address of the object
MOV EAX,[EBX].strTitle			;copies the information in title into the bookorder
ASSUME EBX:nothing				;to return the system to assuming nothing
RET
getTitle ENDP

;* Method Name: getQuantity
;* Method Purpose: A getter for the quantity attribute
;*
;* Date created: 12/4/2015
;*
;* @param ths, the object's address
;*	
;* @return the quantity stored in the object
getQuantity PROC Near32 stdcall uses ebx, ths:dword
ASSUME EBX: ptr bookorder		;so that the parameters at the address stored in ebx can be changed
MOV EBX,ths						;EBX = address of the object
MOV EAX,[EBX].quantity			;EAX = quantity
ASSUME EBX:nothing				;to return the system to assuming nothing
RET
getQuantity ENDP

;* Method Name: getCostPerBook
;* Method Purpose: A getter for the quantity attribute
;*
;* Date created: 12/4/2015
;*
;* @param ths, the object's address
;*	
;* @return the cost stored in the object
getCostPerBook PROC Near32 stdcall uses ebx, ths:dword
ASSUME EBX: ptr bookorder		;so that the parameters at the address stored in ebx can be changed
MOV EBX,ths						;EBX = address of the object
MOV EAX,[EBX].costPerBook		;EAX = costPerBook
ASSUME EBX:nothing				;to return the system to assuming nothing
RET
getCostPerBook ENDP

;* Method Name: getWeight
;* Method Purpose: A getter for the quantity attribute
;*
;* Date created: 12/4/2015
;*
;* @param ths, the object's address
;*	
;* @return the weight stored in the object
getWeight PROC Near32 stdcall uses ebx, ths:dword
ASSUME EBX: ptr bookorder		;so that the parameters at the address stored in ebx can be changed
MOV EBX,ths						;EBX = address of the object
MOV EAX,[EBX].weight			;EAX = weight
ASSUME EBX:nothing				;to return the system to assuming nothing
RET
getWeight ENDP

;* Method Name: adjustQuantity
;* Method Purpose: A increase or decrease the quantity by a certain amount
;*
;* Date created: 12/4/2015
;*
;* @param ths, the object's address
;* @param amt, the amount to adjust the quantity by
;*	
;* @return a value that represents if the adjustment worked, -1 means it did not
adjustQuantity PROC Near32 stdcall uses ebx, ths:dword, amt:dword
local amount:sdword
ASSUME EBX: ptr bookorder		;so that the parameters at the address stored in ebx can be changed
MOV EBX,ths						;EBX = address of the object
MOV EAX,[EBX].quantity			;EAX = quantity
MOV amount,EAX					;amount = quantity
MOV EAX,amt						;EAX = amt
ADD amount,EAX					;amount = quantity + amt
.IF amount < 0 					;if the total is less than zero
MOV EAX,-1 						;meaning the adjustment resulted in a negative and thus was not done
.ELSE
MOV EAX,amt						;EAX = amt
ADD [EBX].quantity,EAX			;quantity += amt
MOV EAX,1						;meaning that it worked
.ENDIF
ASSUME EBX:nothing				;to return the system to assuming nothing
RET
adjustQuantity ENDP

;* Method Name: totalWeight
;* Method Purpose: Calculates the total weight of the order
;*
;* Date created: 12/4/2015
;*
;* @param ths, the object's address
;*	
;* @return the total weight of the order
totalWeight PROC Near32 stdcall uses  ebx edx, ths:dword
ASSUME EBX: ptr bookorder		;so that the parameters at the address stored in ebx can be changed
MOV EBX,ths						;EBX = address of the object
MOV EDX,[EBX].weight			;EDX = weight
MOV EAX,[EBX].quantity			;EAX = quantity
MUL EDX							;EDX:EAX = EAX * EDX = weight * quantity
ASSUME EBX:nothing				;to return the system to assuming nothing
RET
totalWeight ENDP

;* Method Name: calcCost
;* Method Purpose: A calculates the cost of an order
;*
;* Date created: 12/4/2015
;*
;* @param ths, the object's address
;*	
;* @return the total cost
calcCost PROC Near32 stdcall uses ebx edx, ths:dword
ASSUME EBX: ptr bookorder		;so that the parameters at the address stored in ebx can be changed
MOV EBX,ths						;EBX = address of the object
MOV EDX,[EBX].quantity			;EDX = quantity
MOV EAX,[EBX].costPerBook		;EAX = costPerBook
MUL EDX							;EDX:EAX = EAX * EDX = quantity * costPerBook
ASSUME EBX:nothing				;to return the system to assuming nothing
RET
calcCost ENDP

;* Method Name: shippingCost
;* Method Purpose: A calculates the cost + shipping
;*
;* Date created: 12/4/2015
;*
;* @param ths, the object's address
;*	
;* @return the cost + shipping
shippingCost PROC Near32 stdcall uses ebx edx, ths:dword, amt:dword

RET
shippingCost ENDP

;* Method Name: equals
;* Method Purpose: To check if two objects are equal
;*
;* Date created: 12/4/2015
;*
;* @param ths, the calling object's address
;* @param b, the bookorder to compare to
;*	
;* @return a value that represents if the bookorders are equal, 0 is false, 1 is true
equals PROC Near32 stdcall uses ebx ecx edx esi edi, ths:dword, b:dword
local bool:dword
ASSUME EBX: ptr bookorder		;so that the parameters at the address stored in ebx can be changed
ASSUME EDX: ptr BookOrder		;so that the parameters at the address stored in edx can be changed
INVOKE strLength, [EBX].author	;EAX = length of ths.author
MOV ESI,EAX						;ESI = EAX
MOV bool,1						;bool = 1 = true
MOV EBX,ths						;EBX = address of the object
MOV EDX,b						;EDX = address of b
MOV EAX,[EDX].quantity			;EAX = b.quantity
MOV EDI,[EDX].costPerBook		;EDI = b.costPerBook
MOV ECX,[EDX].weight			;ECX = b.weight
.IF [EBX].quantity != EAX || [EBX].costPerBook != EDI || [EBX].weight != ECX
MOV bool,0						;bool = 0 = false
.ENDIF
MOV ECX,[EBX].author			;ECX = address of ths.author
MOV EDI,[EBX].author			;EDI = address of b.author
.WHILE ESI != 0					;once for every letter in author
MOV AL,[EDI + ESI]				;can't do mem to mem
.IF	[ECX + ESI] != AL			;if each letter of each of the authors is not exactly the same
MOV bool,0						;bool = 0 = false
.ENDIF
DEC ESI							;ESI--
.ENDW
INVOKE strLength, [EBX].strTitle;EAX = length of ths.author
MOV ESI,EAX						;ESI = EAX
MOV ECX,[EBX].strTitle			;ECX = address of ths.title
MOV EDI,[EDX].strTitle			;EDI = address of b.title
.WHILE ESI != 0					;once for every letter in title
MOV AL,[EDI + ESI]				;can't do mem to mem
.IF	[ECX + ESI] != AL			;if each letter of each of the titles is not exactly the same
MOV bool,0						;bool = 0 = false
.ENDIF
DEC ESI							;ESI--
.ENDW
ASSUME EBX:nothing				;to return the system to assuming nothing
ASSUME EDX:nothing
MOV EAX,bool					;EAX gets the answer
RET
equals ENDP

;* Method Name: strLength
;* Method Purpose: determines the length of a string based on its address
;*
;* Date created: 12/1/2015
;*
;* @param string, the address of a string
;*
;* @return the length of a given string	
strLength PROC Near32 stdcall uses ebx edx, string:dword
MOV EBX, string					;EBX = address of the string
MOV DL, [EBX]					;DL = first char in string
MOV EAX,1						;since it is now one letter long
.WHILE DL != 0					;until the null terminated character is found
	MOV DL,[EBX + EAX]			;DL = [string + counter]
	INC EAX						;one more letter has been added
.ENDW
RET
strLength ENDP

;* Method Name: strcpy
;* Method Purpose: copies the contents of a source string into a destination
;*				  string.
;*
;* Date created: 12/1/2015
;*
;* @param strSource, the address of the string to copy
;* @param strDest, the address to put the copy
;*
strcpy PROC Near32 stdcall uses eax ebx edx, strDest:dword, strSource:dword
MOV EBX, [strSource]			;EBX = address of strSource
MOV EDX, [strDest]				;EDX = address of strDest
MOV AL, [EBX]					;al = first char in strSource
MOV [EDX],AL					;char in strDest = char in strSource
.WHILE AL != 0					;until the null terminated character is found
	MOV AL,[EBX]				;get the next character in AL
	INC EBX						;one more letter has been added
	MOV [EDX],AL				;char in strDest = char in strSource
	INC EDX						;go to the next character in EDX
.ENDW
RET
strcpy ENDP

END