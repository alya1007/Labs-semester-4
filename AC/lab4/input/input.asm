section .data ; Start of the .data section
    prompt db 'Enter a string: ' ; Define a prompt message as a null-terminated string of ASCII characters
    prompt_len equ $-prompt ; Compute the length of the prompt message
    buffer times 256 db 0 ; Define a buffer to store the user's input, with a maximum size of 256 bytes

section .text ; Start of the .text section
    global _start ; Declare the entry point of the program as _start

_start: ; Start of the _start subroutine
    ; Display the prompt message
    mov eax, 4 ; Move the system call number for write to EAX
    mov ebx, 1 ; Move the file descriptor for standard output to EBX
    mov ecx, prompt ; Move the address of the prompt message to ECX
    mov edx, prompt_len ; Move the length of the prompt message to EDX
    int 0x80 ; Call the Linux kernel's system call handler

    ; Read input from the user
    mov eax, 3 ; Move the system call number for read to EAX
    mov ebx, 0 ; Move the file descriptor for standard input to EBX
    mov ecx, buffer ; Move the address of the buffer to store the user's input to ECX
    mov edx, 255 ; Move the maximum number of bytes to read to EDX
    int 0x80 ; Call the Linux kernel's system call handler

    ; Display the user's input
    mov eax, 4 ; Move the system call number for write to EAX
    mov ebx, 1 ; Move the file descriptor for standard output to EBX
    mov ecx, buffer ; Move the address of the buffer containing the user's input to ECX
    int 0x80 ; Call the Linux kernel's system call handler

    ; Exit the program
    mov eax, 1 ; Move the system call number for exit to EAX
    xor ebx, ebx ; Clear EBX (equivalent to setting it to zero)
    int 0x80 ; Call the Linux kernel's system call handler to terminate the program
