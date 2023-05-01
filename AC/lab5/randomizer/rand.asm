section .data
    buffer: times 11 db 0   ; Buffer to hold the ASCII characters to print

section .text
    global _start

_start:
    ; Initialize random number generator with current system time
    mov eax, 42h
    xor ebx, ebx
    int 0x80                ; Use Linux system call to get current system time
    mov ecx, eax            ; Use system time as seed value

    ; Generate 10 random numbers from 1 to 55
    mov eax, ecx            ; Use system time as initial value for random number generator
    mov edx, 0              ; Initialize upper 32 bits of result to 0
    mov ecx, 10             ; Loop counter
generate:
    ; Generate random number
    mov ebx, 1103515245
    mul ebx
    add eax, 12345
    mov ebx, eax
    shr eax, 16
    add eax, ebx
    mov ebx, 0x7fffffff     ; Use 2^31-1 as the maximum value for scaling
    div ebx
    inc eax                 ; Add 1 to get range 1-55

    ; Convert random number to ASCII character
    add eax, 48             ; Convert to ASCII code
    mov byte [buffer + ecx - 1], al  ; Store the ASCII character in the buffer

    ; Loop
    loop generate

    ; Print the ASCII characters stored in the buffer
    mov eax, 4              ; System call for write
    mov ebx, 1              ; File descriptor for stdout
    mov ecx, buffer         ; Address of the buffer
    mov edx, 10             ; Length of the buffer
    int 0x80

    ; Exit program
    mov eax, 1              ; System call for exit
    xor ebx, ebx            ; Return code 0
    int 0x80
