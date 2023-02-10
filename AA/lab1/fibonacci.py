from decimal import ROUND_HALF_EVEN, Context, Decimal
import time
import matplotlib.pyplot as plt
import math


def fibonacci(n):
    if n <= 1:
        return n
    return fibonacci(n-1) + fibonacci(n-2)


def fibonacci_dp(n):
    if n == 0:
        return 0
    elif n == 1:
        return 1
    else:
        fib = [0] * (n + 1)
        fib[0] = 0
        fib[1] = 1
        for i in range(2, n + 1):
            fib[i] = fib[i - 1] + fib[i - 2]
        return fib[n]


def fibonacci_binet(n):
    ctx = Context(prec=60, rounding=ROUND_HALF_EVEN)
    phi = Decimal((1 + Decimal(math.sqrt(5))))
    phi1 = Decimal((1 - Decimal(math.sqrt(5))))
    return int((ctx.power(phi, Decimal(n)) - ctx.power(phi1, Decimal(n)))/(2**n * Decimal(5**(1/2))))


def fibonacci_iterative(n):
    if n == 0:
        return 0
    elif n == 1:
        return 1
    else:
        a, b = 0, 1
        for i in range(2, n+1):
            c = a + b
            a = b
            b = c
        return b


def fibonacci_matrix(n):
    if n == 0:
        return 0
    else:
        A = [[1, 1], [1, 0]]
        F = power(A, n - 1)
        return F[0][0]
    

def fib_memo(n, cache={0:0, 1:1}): 
    if n in cache: 
        return cache[n]
    cache[n] = fib_memo(n - 1, cache) + fib_memo(n - 2, cache) 
    return cache[n]


def fibonacci_tail_call(n, a=0, b=1):
    if n == 0:
        return a
    elif n == 1:
        return b
    else:
        return fibonacci_tail_call(n-1, b, a + b)


def executionTime(n, function):
    st = time.time()
    function(n)
    et = time.time()
    return (et-st)


def tableTime(x, y, function):
    for n in x:
        execTime = executionTime(n, function)
        y.append(execTime)
        print(f'{execTime:10.4}', end='')


def multiplyMatrix(A, B):
    C = [[0, 0], [0, 0]]
    for i in range(2):
        for j in range(2):
            for k in range(2):
                C[i][j] += A[i][k] * B[k][j]
    return C


def power(A, n):
    if n == 0 or n == 1:
        return A
    else:
        B = power(A, n // 2)
        C = multiplyMatrix(B, B)
        if n % 2 == 0:
            return C
        else:
            return multiplyMatrix(A, C)


def drawGraph(x, y):
    plt.plot(x, y, marker='o', markerfacecolor='blue', markersize=5)
    plt.xlabel('n-th Fibonacci Term')
    plt.ylabel('Time (s)')
    plt.show()


if __name__ == "__main__":
    x = [5, 7, 10, 12, 15, 17, 20, 22, 25, 27, 30, 32, 35,
        37, 40, 42, 45]

    # x = [501, 631, 794, 1000,
    #     1259, 1585, 1995, 2512, 3162,
    #     3981, 5012, 6310, 7943, 10000, 12589, 15849]

    print('n:    ', end='')
    for n in x:
        print(f'{n:10}', end='')

    for i in range(0, 3):
        print('\nsec:  ', end='')
        y = []
        # tableTime(x, y, fibonacci)
        # tableTime(x, y, fibonacci_dp)
        # tableTime(x, y, fibonacci_matrix)
        # tableTime(x, y, fibonacci_binet)
        # tableTime(x, y, fibonacci_iterative)
        # tableTime(x, y, fib_memo)
        tableTime(x, y, fibonacci_tail_call)

    drawGraph(x, y)
