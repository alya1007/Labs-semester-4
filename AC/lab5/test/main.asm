section .data
    num1 db 10
    num2 db 5
    sum db 0

section .text
    global _start

_start:
    ; add the numbers
    mov al, byte[num1]
    add al, byte[num2]
    mov byte[sum], al

    ; display the sum
    mov eax, 4      ; system call for write
    mov ebx, 1      ; file descriptor for stdout
    mov ecx, sum    ; address of sum variable
    mov edx, 1      ; number of bytes to write
    int 0x80

    ; exit program
    mov eax, 1      ; system call for exit
    xor ebx, ebx    ; return 0 status
    int 0x80
