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


def monte_carlo_pi(n: int) -> float:
    import random
    import math

    in_circle = 0
    total = 0
    while True:
        x = random.random()
        y = random.random()
        if x**2 + y**2 <= 1:
            in_circle += 1
        total += 1
        pi_approx = 4 * in_circle / total
        if round(pi_approx, n) == round(math.pi, n):
            return pi_approx


def bbp_pi(n: int) -> str:
    from decimal import Decimal, getcontext

    # Set the precision to n+1 digits
    getcontext().prec = n + 1

    pi = Decimal(0)
    for k in range(n + 1):
        pi += (Decimal(1) / 16**k) * (
            (Decimal(4) / (8 * k + 1))
            - (Decimal(2) / (8 * k + 4))
            - (Decimal(1) / (8 * k + 5))
            - (Decimal(1) / (8 * k + 6))
        )

    return str(pi)[-1]


def measure_time(func, n):
    import time

    start = time.time()
    func(n)
    end = time.time()
    return end - start


def print_results(func):
    print("{:<6s} {:>7s}".format("Size", "Time"))
    y = []
    for i in x:
        y.append(measure_time(func, i))
        print("{:<6d} {:>10.5f}".format(i, measure_time(compute_pi, i)))
    plot_results(x, y)


def plot_results(x, y):
    import matplotlib.pyplot as plt

    plt.plot(x, y)
    plt.xlabel("Number of Digits")
    plt.ylabel("Time in Seconds")
    plt.show()


if __name__ == "__main__":
    import matplotlib.pyplot as plt

    x = []
    for i in range(11):
        x.append(i)

    # print_results(compute_pi)
    # print_results(monte_carlo_pi)
    # print_results(bbp_pi)

    # show graph with all three functions
    y1 = []
    y2 = []
    y3 = []
    for i in x:
        y1.append(measure_time(compute_pi, i))
        y2.append(measure_time(monte_carlo_pi, i))
        y3.append(measure_time(bbp_pi, i))
    plt.plot(x, y1, label="Chudnovsky")
    plt.plot(x, y2, label="Monte Carlo")
    plt.plot(x, y3, label="BBP")
    plt.xlabel("Number of Digits")
    plt.ylabel("Time in Seconds")
    plt.legend()
    plt.show()
