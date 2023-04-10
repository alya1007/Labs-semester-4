bits 64
default rel

extern printf
extern GetStdHandle
extern WriteConsoleA
; 64-bit Windows assembly program to concatenate 'a', 'b', and 'c'

section .data
    msg1 db 'a'
    msg2 db 'b'
    msg3 db 'c'
    newline db 0x0A
    result db 3 dup(0)

section .text
    global main
    extern ExitProcess
    extern GetStdHandle
    extern WriteConsoleA
    extern ReadConsoleA

main:
    ; Get the handle to the console output
    xor ecx, ecx ; Set ECX to 0
    mov eax, -11 ; STD_OUTPUT_HANDLE
    call GetStdHandle
    mov r8, rax ; Save the handle to r8

    ; Copy 'a' to result
    mov rdi, result
    mov rsi, msg1
    mov rcx, 1
    rep movsb

    ; Copy 'b' to result
    mov rdi, result + 1
    mov rsi, msg2
    mov rcx, 1
    rep movsb

    ; Copy 'c' to result
    mov rdi, result + 2
    mov rsi, msg3
    mov rcx, 1
    rep movsb

    ; Write a newline to the console
    mov rcx, newline
    mov edx, 1
    xor ebx, ebx ; Set EBX to 0
    push rbx ; lpNumberOfCharsWritten (not used)
    push rcx ; lpBuffer
    push rdx ; nNumberOfCharsToWrite
    push r8 ; hConsoleOutput
    call WriteConsoleA

    ; Display the resulting string "abc"
    mov rcx, result
    mov edx, 3
    xor ebx, ebx ; Set EBX to 0
    push rbx ; lpNumberOfCharsWritten (not used)
    push rcx ; lpBuffer
    push rdx ; nNumberOfCharsToWrite
    push r8 ; hConsoleOutput
    call WriteConsoleA

    ; Wait for user input before exiting
    xor ecx, ecx ; Set ECX to 0
    xor edx, edx ; Set EDX to 0
    lea rdi, [rsp+8] ; lpNumberOfCharsRead
    mov rsi, rsp ; lpBuffer
    mov dword [rsp], 1 ; nNumberOfCharsToRead
    push rdx ; pInputControl (not used)
    push rdi ; lpNumberOfCharsRead
    push dword 0 ; lpOverlapped (not used)
    push rsi ; lpBuffer
    push r8 ; hConsoleInput
    call ReadConsoleA

    ; Exit the program
    xor ecx, ecx ; Set ECX to 0
    call ExitProcess
