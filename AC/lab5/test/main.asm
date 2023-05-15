section .data
    prompt db "Enter a string: ", 0
    output db "Reversed string: ", 0
    input db 100
    nl db 10, 0

section .bss
    buffer resb 100

section .text
    global _start

_start:
    ; Display prompt and read input
    mov eax, 4
    mov ebx, 1
    mov ecx, prompt
    mov edx, 16
    int 0x80

    mov eax, 3
    mov ebx, 0
    mov ecx, input
    mov edx, 100
    int 0x80

    ; Find length of input
    xor eax, eax
    xor ebx, ebx
    xor ecx, ecx
    mov esi, input
    cld
    repne scasb
    not ecx
    dec ecx

    ; Reverse input
    mov edi, buffer
    mov ebx, ecx
    add esi, ecx
    dec esi

    loop_reverse:
        mov al, [esi]
        mov [edi], al
        inc edi
        dec esi
        loop loop_reverse

    ; Display output
    mov eax, 4
    mov ebx, 1
    mov ecx, output
    mov edx, 16
    int 0x80

    mov eax, 4
    mov ebx, 1
    mov ecx, buffer
    mov edx, ecx
    int 0x80

    ; Exit program
    mov eax, 1
    xor ebx, ebx
    int 0x80
