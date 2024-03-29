SYS_EXIT        equ 1
SYS_READ        equ 3
SYS_WRITE       equ 4
SYS_TIME_OF_DAY equ 96
STDIN           equ 0
STDOUT          equ 1

segment .data
    initialized_array db 15, 13, 2, 5, 7, 9, 11, 4, 6, 8, 10, 12, 14, 1, 3
    array_sum dq 0

    ; messages for prompts
    nl db 10, 0
    nl_len equ $- nl

    menu_msg db "Select an action:", 10, "0: Random numbers", 10, "1: Concatenate strings", 10, "2: Add a prefix to a string", 10, "3: Integer to string", 10, "4: Remove spaces", 10, "5: Generate a random string", 10, "6: Add sufix", 10, "7: Reverse string", 10, "8: Array sum", 10, "9: Generate a random number", 10, 10, 0

    invalid_msg db "Invalid choice! You chose ", 0

    result_msg db "Result: ", 0

    exit_msg db "Goodbye!", 10, 10, 0

    random_msg db "Random number: ", 0

    comma_separator db ", ", 0

    string_prompt_msg db "Enter a string: ", 0

    string_prompt2_msg db "Enter another string: ", 0

    string_len_msg db "String length: ", 0

    reverse_msg db "Reverse string : ", 0

    number_msg db "Enter a number: ", 0

    prefix_msg db "Enter a prefix: ", 0

    str_to_int_msg db "Enter a number: ", 0
    dot db ".", 0

    array_length_msg db "Enter the length of the array: ", 0
    array_element_msg db "Enter an element (up to 255): ", 0
    not_found_msg db "Element not found!", 0

    find_element_msg db "Here's an array: [15, 13, 2, 5, 7, 9, 11, 4, 6, 8, 10, 12, 14, 1, 3]", 10, "Enter an element to find: ", 0
    find_element_result_msg db "Element found at index (0 based): ", 0

    random_msg_2 db "Random number: ", 0
    random_msg_len_2 equ $ - random_msg

    space db ' ', 0  ; space character


segment .bss
    choice resb 4     ; string to store choice input
    string resb 255   ; string to store input
    string2 resb 255  ; another buffer string
    string3 resb 255  ; guess what
    byte_arr resb 255 ; byte array to store numbers

segment .text
    global _start

print:
    ; Prints null terminated string
    ; Input: rdi = address of string to print

    push rax
    push rbx
    push rcx
    push rdx

    ; Get string length
    call str_len

    mov edx, eax
    mov ecx, edi,
    mov eax, SYS_WRITE
    mov ebx, STDOUT
    int 0x80

    pop rdx
    pop rcx
    pop rbx
    pop rax

    ret

print_newline:
    ; Push registers to stack
    push rax
    push rbx
    push rcx
    push rdx

    ; Print a newline
    mov eax, SYS_WRITE
    mov ebx, STDOUT
    mov ecx, nl
    mov edx, nl_len
    int 0x80

    ; Pop registers from stack
    pop rdx
    pop rcx
    pop rbx
    pop rax

    ret

reverse_str:
    ; Reverses a string
    ; Input: rdi = address of string to reverse
    ;        rsi = address of string to store result

    ; Output: rsi = address of string containing result
    ;         rax = length of string

    push rsi    ; Save the address of the result string
    push rcx    ; Save any existing value of rcx on the stack
    ; Cycle through the string to count the number of characters
    xor rcx, rcx; string length
    _reverse_str_loop:
    cmp byte [rdi + rcx], 0
    je _reverse_str_done
    inc rcx
    jmp _reverse_str_loop
    _reverse_str_done:
    mov rax, rcx    ; String length
    _reverse_str_copy:
    ; Traverse the string backwards and copy each character to the result string
    mov bl, [rdi + rcx]
    mov [rsi], bl
    inc rsi
    dec rcx
    cmp rcx, -1
    jne _reverse_str_copy
    ; Add a null terminator to the end of the result string
    mov byte [rsi], 0
    ; Restore register values
    pop rcx
    pop rsi
    ret

concat_str:
    ; Concatenates two strings putting the output in a third
    ; Input: rdi = address of first string
    ;        rsi = address of second string
    ;        rdx = address of string to store result
    ; Output: rdx = address of string containing result

    push rdx        ; Save the address of the result string
    push rdi        ; Save the address of the first string
    push rsi        ; Save the address of the second string

    ; Copy the first string to the result string

    _concat_str_copy_first:
    mov bl, [rdi]
    cmp bl, 10      ; ignore newlines

    ; copy byte if not newline
    je _concat_str_skip_newline
    mov [rdx], bl
    inc rdx
    _concat_str_skip_newline:
    inc rdi
    cmp byte [rdi], 0
    jne _concat_str_copy_first

    ; Copy the second string to the result string

    _concat_str_copy_second:
    mov bl, [rsi]
    cmp bl, 10      ; ignore newlines

    ; copy byte if not newline
    je _concat_str_skip_newline2
    mov [rdx], bl
    inc rdx
    _concat_str_skip_newline2:
    inc rsi
    cmp byte [rsi], 0
    jne _concat_str_copy_second

    ; Add a null terminator to the end of the result string
    mov byte [rdx], 0

    ; Restore register values
    pop rsi
    pop rdi
    pop rdx

    ret

int_to_str:
    ; Convert an integer to a string
    ; Input: rdi = integer to convert
    ;        rsi = address of string to store result
    ; Output: rsi = address of string containing result
    ;         rax = length of string
    push rcx        ; Save any existing value of rcx on the stack
    push rsi        ; Save the address of the result string
    push rdi
    push rdx

    xor rcx, rcx    ; Result string length
    mov rax, rdi    ; Copy the input value to rax
    mov rdi, 10     ; Reuse as divisor
    imul rdi        ; SHITTY CODE
    _int_to_str_loop:
    xor rdx, rdx    ; Clear rdx
    div rdi         ; Divide rax by rbx
    push rdx        ; Push the remainder on the stack
    inc rcx         ; Increment the result string length
    cmp rax, 0      ; Compare rax to 0
    jne _int_to_str_loop
    ; Store the string length in rax
    mov rax, rcx
    ; Pop the remainders off the stack and add '0' to convert them to ASCII
    _int_to_str_pop:
    pop rdx
    add dl, '0'
    mov [rsi], dl
    inc rsi
    dec rcx
    cmp rcx, 0
    jne _int_to_str_pop
    ; Add a null terminator to the end of the result string
    mov byte [rsi], 0
    ; Restore register values
    pop rdx
    pop rdi
    pop rsi
    pop rcx
    ret

str_to_int:
    ; Convert a string to an integer
    ; Input: rdi = address of string to convert
    ; Output: rax = integer value

    push rcx    ; Save any existing value of rcx on the stack
    push rsi    ; Save any existing value of rsi on the stack
    push rbx    ; Save any existing value of rbx on the stack

    call str_len   ; Get the length of the string
    mov rcx, rax    ; Copy the length to rcx
    mov rbx, 1      ; Set the multiplier to 1

    xor rsi, rsi    ; Result will be stored in rsi temporarily
    _str_to_int_loop:
    xor rax, rax    ; Clear rax
    dec rcx         ; Decrement rcx
    cmp rcx, -1     ; Compare rcx to -1 (end of cycle)
    je _str_to_int_done
    mov al, [rdi + rcx] ; Copy the character to al
    sub al, '0'     ; Convert the character to an integer
    mul rbx         ; Multiply the digit by the multiplier
    add rsi, rax    ; Add the result to rsi
    mov rax, 10     ; Prepare to multiply the multiplier by 10
    mul rbx         ; Multiply the multiplier by 10
    mov rbx, rax    ; Copy the result to rbx
    jmp _str_to_int_loop

    _str_to_int_done:
    mov rax, rsi    ; Copy the result to rax
    pop rbx         ; Restore rbx
    pop rsi         ; Restore rsi
    pop rcx         ; Restore rcx
    ret

str_len:
    push rcx    ; Save any existing value of rcx on the stack
                ; since it will be used in the procedure
    xor rcx, rcx
_str_len_loop:
    cmp byte [rdi + rcx], 0
    je _str_len_done
    inc rcx
    jmp _str_len_loop
_str_len_done:
    dec rcx         ; Remove null character from count
    mov rax, rcx    ; return value

    pop rcx
    ret

random:
    ; Input: rdi = seed, if 0 then use time of day
    ; Output: rax = random number between 1 and 55
    ;         rdi = new seed

    ; Save register values
    push rbx
    push rcx
    push rdx

    ; Get the unix time as the seed
    cmp rdi, 0
    jne _generate_random
    ; Get the time of day
    mov eax, SYS_TIME_OF_DAY
    mov ebx, 0
    int 0x80
    mov rdi, rax

    _generate_random:
    ; Generate a big random number
    mov rax, rdi
    mov rbx, 134775813
    mov rcx, 1
    mul rbx
    add rax, rcx
    mov rdi, rax
    ; Get the remainder
    mov rbx, 55
    xor rdx, rdx
    div rbx
    inc rdx     ; Increment the remainder to get a number between 1 and 55
    mov rax, rdx

    ; Restore register values
    pop rdx
    pop rcx
    pop rbx
    ret

random_string:
    ; Input: rdi = seed, if 0 then use time of day
    ;        rsi = address of string to store result
    ;        rdx = length of string to generate
    ; Output: rsi = address of string containing result

    ; Save register values
    push rbx
    push rcx
    push rsi

    mov rcx, rdx    ; Copy the length to rcx

    ; Generate a random number that is stored in rax
    ; and add it to the string till the counter reaches 0

    _random_string_loop:
    call random
    mov rbx, 26           ; Number of letters in the alphabet
    xor rdx, rdx
    div rbx              ; Get the remainder
    add dl, 'a'          ; Convert the remainder to a letter
    mov [rsi], dl        ; Store the letter in the string
    inc rsi              ; Increment the string pointer
    cmp rcx, 0           ; Compare the counter to 0
    dec rcx              ; Decrement the counter
    jne _random_string_loop

    ; Add a null terminator to the end of the string
    mov byte [rsi], 0

    ; Restore register values
    pop rsi
    pop rcx
    pop rbx
    ret

find_array_index:
    ; Find index of element in the array 'initialized_array'
    ; Input: rdi = element to find
    ; Output: rax = index of element in array, -1 if not found

    push rcx    ; Save any existing value of rcx on the stack
    mov rcx, 0  ; Set the counter to 0

    _find_array_index_loop:
    cmp rcx, 15 ; Compare the counter to 15 (length of array)
    je _find_array_index_not_found

    xor rax, rax ; Clear rax
    mov al, [initialized_array + rcx] ; Copy the element to rax
    cmp rax, rdi ; Compare the element to the one we are looking for
    je _find_array_index_found
    inc rcx      ; Increment the counter
    jmp _find_array_index_loop

    _find_array_index_found:
    mov rax, rcx ; Copy the index to rax
    jmp _find_array_index_done

    _find_array_index_not_found:
    mov rax, -1 ; Set the index to -1

    _find_array_index_done:
    pop rcx     ; Restore rcx
    ret

str_to_uppercase:
    ; Input: rdi = address of string to convert
    ;        rsi = address of string to store result
    ; Output: rsi = address of string containing result

    push rcx    ; Save any existing value of rcx on the stack
    xor rcx, rcx

    _str_to_uppercase_loop:
    cmp byte [rdi + rcx], 0 ; Compare the character to 0
    je _str_to_uppercase_done
    mov al, [rdi + rcx]     ; Copy the character to al
    cmp al, 'a'             ; Compare the character to 'a'
    jl _str_to_uppercase_not_lowercase
    cmp al, 'z'             ; Compare the character to 'z'
    jg _str_to_uppercase_not_lowercase
    sub al, 32              ; Convert the character to uppercase
    mov [rsi + rcx], al     ; Store the character in the string
    inc rcx                 ; Increment the counter
    jmp _str_to_uppercase_loop

    _str_to_uppercase_not_lowercase:
    mov [rsi + rcx], al     ; Store the character in the string
    inc rcx                 ; Increment the counter
    jmp _str_to_uppercase_loop

    _str_to_uppercase_done:
    mov byte [rsi + rcx], 0 ; Add a null terminator to the end of the string

    pop rcx     ; Restore rcx
    ret

_start:
    xor rdi, rdi         ; Initial seed to 0
prompt:
    ; Save seed from previous randoms
    push rdi
    ; Write menu
    mov rdi, menu_msg
    call print

    mov eax, SYS_READ    ; System read
    mov ebx, STDIN       ; System input
    mov ecx, choice      ; Where to store input
    mov edx, 4           ; Length to read
    int 0x80             ; Interupt Kernel

    mov rdi, choice      ; Move choice into rdi
    call str_to_int      ; Call str_to_int procedure
    pop rdi              ; Restore seed

    cmp eax, 0           ; If choice is 0
    je random_prompt     ; Jump to random_prompt
    cmp eax, 1           ; If choice is 1
    je concat_str_prompt    ; Jump to concat_str_prompt
    cmp eax, 2           ; If choice is 2
    je string_prefix_prompt ; Jump to string_prefix_prompt
    cmp eax, 3           ; If choice is 3
    je str_to_int_prompt ; Jump to str_to_int_prompt
    cmp eax, 4           ; If choice is 4
    je remove_spaces ; Jump to remove_spaces
    cmp eax, 5           ; If choice is 5
    je random_string_prompt ; Jump to random_string_prompt
    cmp eax, 6           ; If choice is 6
    je add_suffix ; Jump to add_suffix
    cmp eax, 7           ; If choice is 7
    je reverse_string_prompt ; Jump to string_prefix_prompt
    cmp eax, 8           ; If choice is 8
    je array_sum_prompt  ; Jump to array_sum_prompt
    cmp eax, 9           ; If choice is 9
    je print_random_number ; Jump to find_array_index_prompt

    jmp invalid          ; Else jump to invalid

random_prompt:
    push rcx            ; Save any existing value of rcx on the stack
    push rdi            ; store seed
    ; print random number
    mov rdi, random_msg
    call print

    xor rcx, rcx        ; Clear rcx counter

    _random_loop:
    pop rdi             ; Restore seed
    call random         ; Call random procedure
    push rdi            ; Store seed

    mov rdi, rax        ; Move random number into rdi
    mov rsi, string     ; Move string pointer into rsi
    call int_to_str     ; Call int_to_str procedure
    mov rdi, string     ; Move string pointer into rdi
    call print          ; Call print procedure

    inc rcx             ; Increment rcx counter
    cmp rcx, 9          ; Compare rcx to 9
    je _random_end

    ; print comma
    mov rdi, comma_separator
    call print
    jmp _random_loop

    _random_end:
    call print_newline
    pop rdi
    pop rcx
    jmp prompt          ; Jump to prompt

reverse_string_prompt:
    ; Write string prompt
    mov rdi, string_prompt_msg
    call print

    ; Read string
    mov eax, SYS_READ
    mov ebx, STDIN
    mov ecx, string      ; Where to store input
    mov edx, 255         ; Max length to read
    int 0x80

    ; Write reverse string message
    mov rdi, reverse_msg
    call print

    mov rdi, string      ; Move string pointer into rdi
    mov rsi, string2     ; Move string2 pointer into rsi
    call reverse_str

    mov rdi, string2     ; Move string2 pointer into rdi
    call print
    call print_newline

    jmp prompt           ; Jump to prompt

str_to_int_prompt:
    ; Write prompt

    mov rdi, str_to_int_msg
    call print

    ; Read string
    mov eax, SYS_READ
    mov ebx, STDIN
    mov ecx, string      ; Where to store input
    mov edx, 255         ; Max length to read
    int 0x80

    mov rdi, string      ; Move string pointer into rdi
    call str_to_int      ; Call str_to_int procedure

    mov rcx, rax         ; Move result into counter register
    mov rdi, dot         ; Move dot pointer into rdi

    _str_to_int_prompt_loop:
    call print           ; Call print procedure
    dec rcx              ; Decrement counter
    cmp rcx, 0           ; Compare counter to 0
    jne _str_to_int_prompt_loop ; Jump if not equal to 0

    call print_newline
    jmp prompt           ; Jump to prompt


concat_str_prompt:
    ; Write string prompt
    mov rdi, string_prompt_msg
    call print

    ; Read string
    mov eax, SYS_READ
    mov ebx, STDIN
    mov ecx, string      ; Where to store input
    mov edx, 255         ; Max length to read
    int 0x80

    ; Write another string prompt

    mov rdi, string_prompt2_msg
    call print

    ; Read string
    mov eax, SYS_READ
    mov ebx, STDIN
    mov ecx, string2     ; Where to store input
    mov edx, 255         ; Max length to read
    int 0x80

    ; Write result message
    mov rdi, result_msg
    call print

    ; Concat strings in string3
    mov rdi, string      ; Move string pointer into rdi
    mov rsi, string2     ; Move string2 pointer into rsi
    mov rdx, string3     ; Move string3 pointer into rdx
    call concat_str

    mov rdi, string3     ; Move string3 pointer into rdi
    call print           ; Call print procedure

    call print_newline
    jmp prompt           ; Jump to prompt

random_string_prompt:
    ; Write prompt
    mov rdi, number_msg
    call print

    ; Read number
    mov eax, SYS_READ
    mov ebx, STDIN
    mov ecx, string      ; Where to store input
    mov edx, 255         ; Max length to read
    int 0x80

    ; Convert string to int
    mov rdi, string      ; Move string pointer into rdi
    call str_to_int      ; Call str_to_int procedure

    ; Set arguments for the random_string procedure
    xor rdi, rdi         ; Clear rdi
    mov rsi, string      ; Move string pointer into rsi
    mov rdx, rax         ; Move number into rdx

    call random_string   ; Call random_string procedure

    mov rdi, string      ; Move string pointer into rdi
    call print           ; Call print procedure
    call print_newline

    jmp prompt           ; Jump to prompt

string_prefix_prompt:
    ; Write string prompt
    mov rdi, string_prompt_msg
    call print

    ; Read string
    mov eax, SYS_READ
    mov ebx, STDIN
    mov ecx, string      ; Where to store input
    mov edx, 255         ; Max length to read
    int 0x80

    ; Write prefix prompt
    mov rdi, prefix_msg
    call print

    ; Read prefix
    mov eax, SYS_READ
    mov ebx, STDIN
    mov ecx, string2     ; Where to store input
    mov edx, 255         ; Max length to read
    int 0x80

    ; Write result message
    mov rdi, result_msg
    call print

    ; prepare arguments for concat_str procedure
    mov rdi, string2      ; Move string2 pointer into rdi
    mov rsi, string       ; Move string pointer into rsi
    mov rdx, string3      ; Move string3 pointer into rdx
    call concat_str       ; Call concat_str procedure

    mov rdi, string3      ; Move string3 pointer into rdi
    call print            ; Call print procedure

    jmp prompt           ; Jump to prompt

array_sum_prompt:
    mov rdi, array_length_msg
    call print
    ; Reset counter
    mov qword [array_sum], 0

    ; Read array length
    mov eax, SYS_READ
    mov ebx, STDIN
    mov ecx, string      ; Where to store input
    mov edx, 255         ; Max length to read
    int 0x80

    ; Convert string to int
    mov rdi, string      ; Move string pointer into rdi
    call str_to_int      ; Call str_to_int procedure

    ; Move array length to counter register
    mov rcx, rax

    ; Loop N times to read N numbers and sum them
    _array_sum_prompt_loop:
    mov rdi, number_msg
    call print

    push rcx             ; Save counter

    ; Read number
    mov eax, SYS_READ
    mov ebx, STDIN
    mov ecx, string      ; Where to store input
    mov edx, 255         ; Max length to read
    int 0x80

    pop rcx              ; Restore counter

    ; Convert string to int
    mov rdi, string      ; Move string pointer into rdi
    call str_to_int      ; Call str_to_int procedure

    ; Add number to accumulator
    add qword [array_sum], rax

    ; Decrement counter
    dec rcx

    ; Loop if counter is not 0
    cmp rcx, 0
    jne _array_sum_prompt_loop

    ; Write result message
    mov rdi, result_msg
    call print

    ; Convert accumulator to string
    mov rdi, [array_sum]  ; Move accumulator into rdi
    mov rsi, string
    call int_to_str

    ; Print result
    mov rdi, string
    call print
    call print_newline

    jmp prompt            ; Jump to prompt


print_random_number:
    ; Call the random function to generate a random number
    call random
    ; Move the random number from rax to rdi for printing
    mov rdi, rax
    ; Print the random number
    mov eax, SYS_WRITE
    mov ebx, STDOUT
    mov ecx, random_msg_2
    mov edx, random_msg_len_2
    int 0x80

    ; Call the print function (assuming it's defined elsewhere)
    call print

    ; Jump to the prompt
    jmp prompt
    
invalid:
    mov rdi, invalid_msg

    pop rax; Restore rax
    mov rdi, choice
    call print

    jmp prompt


add_suffix:
    ; Input: rdi = address of input string
    ;        rsi = address of suffix string
    ; Output: rdi = address of string containing result

    push rdx        ; Save the value of rdx on the stack

    ; Find the end of the input string
    xor rax, rax   ; Clear rax
    mov rcx, rdi   ; Copy the address of input string to rcx
    call str_len   ; Get the length of the input string
    add rdi, rax   ; Move rdi to the end of the input string

    ; Copy the suffix string to the end of the input string
    _add_suffix_copy:
    mov dl, [rsi]   ; Copy the byte from the suffix string to dl
    mov [rdi], dl   ; Copy dl to the end of the input string
    inc rsi        ; Increment the suffix string pointer
    inc rdi        ; Increment the input string pointer
    cmp byte [rsi], 0   ; Compare the byte in rsi to 0 (end of suffix string)
    jne _add_suffix_copy

    ; Add a null terminator to the end of the result string
    mov byte [rdi], 0

    ; Restore the value of rdx
    pop rdx

    ret

remove_spaces:
    ; Removes spaces from a string
    ; Input: rdi = address of string to modify

    push rsi        ; Save the address of the original string

    mov rsi, rdi    ; Copy the address of the string to rsi
    xor ecx, ecx    ; Clear ecx (counter for non-space characters)

    _remove_spaces_loop:
    mov al, byte [rsi]  ; Load the current character

    cmp al, 0          ; Check if end of string
    je _remove_spaces_done

    cmp al, byte [space]  ; Compare with space character
    je _remove_spaces_skip

    mov byte [rdi + rcx], al  ; Copy the non-space character to the modified string
    inc rcx                  ; Increment the counter

    _remove_spaces_skip:
    inc rsi               ; Increment the string pointer
    jmp _remove_spaces_loop

    _remove_spaces_done:
    mov byte [rdi + rcx], 0  ; Add null terminator to the modified string

    pop rsi                ; Restore rsi

    ret