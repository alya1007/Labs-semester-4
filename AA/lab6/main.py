def compute_pi(n):
    import decimal

    # generate pi to nth digit
    # Chudnovsky algorihtm to find pi to n-th digit
    decimal.getcontext().prec = n + 1
    C = 426880 * decimal.Decimal(10005).sqrt()
    K = 6.0
    M = 1.0
    X = 1
    L = 13591409
    S = L
    for i in range(1, n):
        M = M * (K**3 - 16 * K) / ((i + 1) ** 3)
        L += 545140134
        X *= -262537412640768000
        S += decimal.Decimal(M * L) / X
    pi = C / S
    return str(pi)[-1]


if __name__ == "__main__":
    print(compute_pi(99))
