    .model flat
includelib msvcrt.lib
time    proto c ,:dword
srand   proto c ,:dword
rand    proto c 
 
    .data
n   equ 100000
x   dw n dup(?)
    .code
public main
main proc
    invoke time, 0
    invoke srand, eax
 
    mov esi, 101
    lea ebx, [x]
    lea edi, [x+n*(type x)]
init:
    invoke rand
    div si
    mov [ebx], dx
    add ebx, type x
    cmp ebx, edi
    jb init
 
    xor ax, ax
    ret
main endp
end