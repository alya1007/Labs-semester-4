section .data
    str1 db 'Hello, '
    str1_len equ $-str1
    str2 db 'you!', 0
    str2_len equ $-str2
    result times 256 db 0

section .text
global _start

_start:
    ; Call the concat function
    call concat

    ; Exit the program
    mov eax, 1
    xor ebx, ebx
    int 0x80

; Define the concat function
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

    ret ; Return from the function
