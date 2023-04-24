section .data
    prompt db 'Enter a string: '
    prompt_len equ $-prompt
    buffer times 256 db 0

section .text
    global _start

_start:
    ; display prompt
    mov eax, 4
    mov ebx, 1
    mov ecx, prompt
    mov edx, prompt_len
    int 0x80
    
    ; read input
    mov eax, 3
    mov ebx, 0
    mov ecx, buffer
    mov edx, 255
    int 0x80
    
    ; display input
    mov eax, 4
    mov ebx, 1
    mov ecx, buffer
    int 0x80
    
    ; exit
    mov eax, 1
    xor ebx, ebx
    int 0x80
