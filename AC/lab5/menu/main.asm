section .data
    menu_msg db 10, 'Choose option:  ', 0
    func_msg db 'function'
    str1 db 'Hello, ', 0
    str1_len equ $-str1
    str2 db 'you!', 0
    str2_len equ $-str2
    result times 256 db 0
    section .text
    global _start

_start:
    ; Display the menu
    mov eax, 4
    mov ebx, 1
    mov ecx, menu_msg
    mov edx, 18
    int 0x80

    ; Read the user's choice
    mov eax, 3
    mov ebx, 2
    mov ecx, choice
    mov edx, 2
    int 0x80

    ; Convert the choice to a number
    movzx eax, byte [choice]
    sub eax, '0'

    ; Jump to the appropriate function based on the choice
    cmp eax, 0
    je concat
    cmp eax, 1
    je func2
    cmp eax, 2
    je func2
    cmp eax, 3
    je func3
    cmp eax, 4
    je func4
    cmp eax, 5
    je func5
    cmp eax, 6
    je func6
    cmp eax, 7
    je func7
    cmp eax, 8
    je func8
    cmp eax, 9
    je func9
    ; If the choice is not valid, go back to the menu
    jmp _start

concat:
    ; Copy the first string to the result buffer
    mov esi, str1
    mov edi, result
    mov ecx, str1_len
    cld
    rep movsb

    ; Copy the second string to the result buffer
    mov esi, str2
    mov edi, result + str1_len
    mov ecx, str2_len
    cld
    rep movsb

    ; Display the concatenated string
    mov eax, 4
    mov ebx, 1
    mov ecx, result
    mov edx, str1_len + str2_len
    int 0x80

    jmp _start

func1:
    ; Call function 2
    mov eax, 4
    mov ebx, 1
    mov ecx, func_msg
    mov edx, 8
    int 0x80
    ; Return to the menu
    jmp _start

func2:
    ; Call function 2
    mov eax, 4
    mov ebx, 1
    mov ecx, func_msg
    mov edx, 8
    int 0x80
    ; Return to the menu
    jmp _start

func3:
    ; Call function 3
    mov eax, 4
    mov ebx, 1
    mov ecx, func_msg
    mov edx, 8
    int 0x80
    ; Return to the menu
    jmp _start

func4:
    ; Call function 4
    mov eax, 4
    mov ebx, 1
    mov ecx, func_msg
    mov edx, 8
    int 0x80
    ; Return to the menu
    jmp _start

func5:
    ; Call function 5
    mov eax, 4
    mov ebx, 1
    mov ecx, func_msg
    mov edx, 8
    int 0x80
    ; Return to the menu
    jmp _start

func6:
    ; Call function 6
    mov eax, 4
    mov ebx, 1
    mov ecx, func_msg
    mov edx, 8
    int 0x80
    ; Return to the menu
    jmp _start

func7:
    ; Call function 7
    mov eax, 4
    mov ebx, 1
    mov ecx, func_msg
    mov edx, 8
    int 0x80
    ; Return to the menu
    jmp _start

func8:
    ; Call function 8
    mov eax, 4
    mov ebx, 1
    mov ecx, func_msg
    mov edx, 8
    int 0x80
    ; Return to the menu
    jmp _start

func9:
    ; Call function 9
    mov eax, 4
    mov ebx, 1
    mov ecx, func_msg
    mov edx, 8
    int 0x80
    ; Return to the menu
    jmp _start

section .bss
choice resb 1
