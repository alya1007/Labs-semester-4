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


def heap_sort(arr):
    # Build a max heap from the input array
    def build_max_heap(arr):
        n = len(arr)
        # Starting from the last non-leaf node, heapify all nodes in the tree
        for i in range(n // 2 - 1, -1, -1):
            heapify(arr, n, i)
    # Heapify a subtree rooted at index i

    def heapify(arr, n, i):
        largest = i
        left = 2 * i + 1
        right = 2 * i + 2
        # If the left child is larger than the parent
        if left < n and arr[left] > arr[largest]:
            largest = left
        # If the right child is larger than the largest so far
        if right < n and arr[right] > arr[largest]:
            largest = right
        # If the largest element is not the root
        if largest != i:
            # Swap the root with the largest element
            arr[i], arr[largest] = arr[largest], arr[i]
            # Heapify the affected subtree
            heapify(arr, n, largest)

    # Make a copy of the input array to avoid modifying it
    arr = arr.copy()
    n = len(arr)
    # Build a max heap from the input array
    build_max_heap(arr)
    # Repeatedly extract the maximum element from the heap and add it to the sorted portion of the output array
    for i in range(n - 1, 0, -1):
        # Swap the root with the last element in the heap
        arr[0], arr[i] = arr[i], arr[0]
        # Heapify the reduced heap
        heapify(arr, i, 0)
    return arr


def counting_sort(arr):
    # Find the range of the input array
    max_val = float('-inf')
    min_val = float('inf')
    for val in arr:
        if val > max_val:
            max_val = val
        if val < min_val:
            min_val = val

    # Create a frequency array to store the count of each distinct element in the input array
    freq = [0] * (int(max_val - min_val) + 1)
    for val in arr:
        freq[int(val - min_val)] += 1

    # Calculate the cumulative sum array by summing up the frequency array elements
    cum_sum = [freq[0]]
    for i in range(1, len(freq)):
        cum_sum.append(cum_sum[-1] + freq[i])

    # Construct the sorted output array by placing each input element in its sorted position based on the cumulative sum array
    sorted_arr = [0] * len(arr)
    for val in arr:
        sorted_arr[cum_sum[int(val - min_val)] - 1] = val
        cum_sum[int(val - min_val)] -= 1

    return sorted_arr


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
    # show_results(x, arrays, merge_sort)
    # show_results(x, arrays, quicksort)
    # show_results(x, arrays, heap_sort)
    show_results(x, arrays, counting_sort)
    # arr = [-13, 2424, -1929, 49499, 333, 212, 23489]
    # print('arr: ', arr)
    # print('quicksort: ', quicksort(arr))


if __name__ == "__main__":
    main()
