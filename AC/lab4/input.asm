global _start   ; Entry point of the program
extern printf   ; Declare the printf function from the C library

section .data   ; Data section, used for storing initialized data
    prompt db "Enter a number: ", 0    ; Define a null-terminated prompt string
    format db "%ld", 0                ; Define a null-terminated format string for scanf
    result_format db "Double of %ld is %ld", 10, 0 ; Define a null-terminated format string for printf

section .bss    ; BSS section, used for storing uninitialized data
    num resq 1  ; Define a 64-bit uninitialized variable for storing the input number

section .text   ; Code section, used for storing instructions
_start:
    ; Prompt the user for a number
    mov rax, 0          ; System call for write
    mov rdi, 1          ; File descriptor for stdout
    mov rsi, prompt     ; Pointer to the prompt string
    mov rdx, 16         ; Length of the prompt string
    syscall             ; Call the kernel to perform the system call

    ; Read the number from stdin
    mov rax, 0          ; System call for read
    mov rdi, 0          ; File descriptor for stdin
    mov rsi, num        ; Pointer to the num variable
    mov rdx, 8          ; Maximum number of bytes to read
    syscall             ; Call the kernel to perform the system call

    ; Double the number
    mov rax, [num]      ; Load the number from memory into rax
    shl rax, 1          ; Multiply the number by 2

    ; Print the result to stdout
    mov rdi, result_format  ; Pointer to the result format string
    mov rsi, [num]      ; First argument for printf (original number)
    mov rdx, rax        ; Second argument for printf (doubled number)
    mov rax, 0          ; System call for printf
    call printf         ; Call the printf function from the C library

    ; Exit the program with a status of 0
    mov rax, 60         ; System call for exit
    xor rdi, rdi        ; Status code (0)
    syscall             ; Call the kernel to perform the system call
