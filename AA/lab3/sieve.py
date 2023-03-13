import matplotlib.pyplot as plt


def eratosthenes_sieve_1(n):
    c = [True] * (n+1)
    c[1] = False
    i = 2
    while i <= n:
        if c[i] == True:
            j = 2 * i
            while j <= n:
                c[j] = False
                j += i
        i += 1
    d = []
    for i in range(1, n+1):
        if c[i] == True:
            d.append(i)
    return d


def eratosthenes_sieve_2(n):
    c = [True] * (n+1)
    c[1] = False
    i = 2
    while i <= n:
        j = 2 * i
        while j <= n:
            c[j] = False
            j += i
        i += 1
    d = []
    for i in range(1, n+1):
        if c[i] == True:
            d.append(i)
    return d


def sundaram_sieve(n):
    c = [True] * (n+1)
    c[1] = False
    i = 2
    while i <= n:
        if c[i] == True:
            j = i + 1
            while j <= n:
                if j % i == 0:
                    c[j] = False
                j += 1
        i += 1
    d = []
    for i in range(1, n+1):
        if c[i] == True:
            d.append(i)
    return d


def eratosthenes_sieve_naive(n):
    c = [True] * (n+1)
    c[0] = False
    c[1] = False
    i = 2
    while i <= n:
        j = 2
        while j < i:
            if i % j == 0:
                c[i] = False
                break
            j += 1
        i += 1
    d = []
    for i in range(1, n+1):
        if c[i] == True:
            d.append(i)
    return d


def segmented_sieve(n):
    import math
    c = [True] * (n+1)
    c[1] = False
    i = 2
    while i <= n:
        j = 2
        while j <= math.sqrt(i):
            if i % j == 0:
                c[i] = False
            j += 1
        i += 1
    d = []
    for i in range(1, n+1):
        if c[i] == True:
            d.append(i)
    return d


def measure_time(func, n):
    import time
    start = time.time()
    func(n)
    end = time.time()
    return end - start


def exec(function, r):
    print()
    print(function.__name__, end="\n")
    print()
    print("{:<6s} {:>11s}".format("Size", "Time"))
    print()
    time_list = []
    for range in r:
        time = measure_time(function, range)
        print("{:<9d} {:>14.8f}".format(range, time))
        print()
        time_list.append(time)
    plt.plot(r, time_list)


def main():
    ranges = [100, 1000, 10000, 100000, 1000000,
              10000000]
    exec(eratosthenes_sieve_1, ranges)


if __name__ == "__main__":
    main()
