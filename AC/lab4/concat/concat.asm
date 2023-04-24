section .data ; Start of the .data section
    str1 db 'Hello, ' ; Define the first string
    str1_len equ $-str1 ; Compute the length of the first string
    str2 db 'you!', 0 ; Define the second string
    str2_len equ $-str2 ; Compute the length of the second string
    result times 256 db 0 ; Define a buffer to store the concatenated string, with a maximum size of 256 bytes

section .text
    global _start

_start:
    ; Copy the first string to the result buffer
    mov esi, str1 ; Move the address of the first string to ESI
    mov edi, result
    mov ecx, str1_len
    cld ; Set the direction flag to forward (incremental)
    rep movsb ; Copy the first string to the result buffer

    ; Copy the second string to the result buffer
    mov esi, str2
    ; Move the address of the result buffer offset by the length of the first string to EDI
    mov edi, result + str1_len
    mov ecx, str2_len ; Move the length of the second string to ECX
    cld
    rep movsb

    ; Display the concatenated string
    mov eax, 4
    mov ebx, 1
    mov ecx, result
    mov edx, str1_len + str2_len ; Move the total length of the concatenated string to EDX
    int 0x80 ; Display the concatenated string

    ; Exit the program
    mov eax, 1 ; Move the system call number for exit to EAX
    xor ebx, ebx ; Clear EBX (equivalent to setting it to zero)
    int 0x80 ; Call the Linux kernel's system call handler to terminate the program
