def merge_sort(a):
    arr = a[:]
    if len(arr) > 1:
        # Divide the array into two halves
        mid = len(arr) // 2
        left_half = arr[:mid]
        right_half = arr[mid:]

        # Recursively sort each half
        merge_sort(left_half)
        merge_sort(right_half)

        # Merge the two sorted halves
        i = j = k = 0  # Initialize indices for left, right, and merged lists
        while i < len(left_half) and j < len(right_half):
            if left_half[i] < right_half[j]:
                arr[k] = left_half[i]
                i += 1
            else:
                arr[k] = right_half[j]
                j += 1
            k += 1

        # Add any remaining elements from left_half
        while i < len(left_half):
            arr[k] = left_half[i]
            i += 1
            k += 1

        # Add any remaining elements from right_half
        while j < len(right_half):
            arr[k] = right_half[j]
            j += 1
            k += 1


def quicksort(arr):
    import random
    # the array is already sorted and can be returned
    if len(arr) <= 1:
        return arr

    # generate a random pivot element from the input array.
    pivot = arr[random.randint(0, len(arr)-1)]
    left, equal, right = [], [], []
    for x in arr:
        if x < pivot:
            left.append(x)
        elif x == pivot:
            equal.append(x)
        else:
            right.append(x)
    # recursively sort the left and right partitions of the
    # input array using the quicksort() function, and concatenate
    # them with the equal partition to obtain the final sorted array.
    return quicksort(left) + equal + quicksort(right)


def generate_random_array(size):
    import random
    arr = []
    for i in range(size):
        arr.append(round(random.uniform(-10000, 10000), 5))
    return arr


def measure_time(func, arr):
    import time
    start = time.time()
    func(arr)
    end = time.time()
    return end - start


def print_results(func, arr, i):
    print("{:<6d} {:>10.5f}".format(
        i, measure_time(func, arr)))


def plot_results(x, y):
    import matplotlib.pyplot as plt
    plt.plot(x, y)
    plt.xlabel("Array Size")
    plt.ylabel("Time in Seconds")
    plt.show()


def show_results(x, array_list, func):
    y = []
    print("{:<6s} {:>7s}".format("Size", "Time"))
    for i in x:
        arr = array_list[x.index(i)]
        y.append(round((measure_time(func, arr)), 5))
        print_results(func, arr, i)
    plot_results(x, y)


def main():

    x = []
    y = []
    arrays = []
    for i in range(500, 7001, 500):
        arrays.append(generate_random_array(i))
        x.append(i)
    show_results(x, arrays, merge_sort)
    show_results(x, arrays, quicksort)
    # arr = [-13, 2424, -1929, 49499, 333, 212, 23489]
    # print('arr: ', arr)
    # print('quicksort: ', quicksort(arr))


if __name__ == "__main__":
    main()
