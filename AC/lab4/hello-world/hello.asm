section .data
    hello db 'Hello, world!',0

section .text
    global _start

_start:
    ; write 'hello' to stdout
    mov eax, 4      ; system call for 'write'
    mov ebx, 1      ; file descriptor for stdout
    mov ecx, hello  ; message to write
    mov edx, 13     ; length of message
    int 0x80        ; call the kernel

    ; exit the program with a status code of 0
    mov eax, 1      ; system call for 'exit'
    xor ebx, ebx    ; status code of 0
    int 0x80        ; call the kernel
