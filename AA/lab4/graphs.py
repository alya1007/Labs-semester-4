import collections
import networkx as nx
import random


def generate_random_graph(num_nodes):
    G = nx.Graph()
    for i in range(num_nodes):
        G.add_node(i)
    for i in range(num_nodes):
        for j in range(i+1, num_nodes):
            if random.random() < 0.5:  # Add an edge with probability 0.5
                G.add_edge(i, j)
    return G


def bfs(graph, start_node):
    visited, queue = set(), collections.deque([start_node])
    visited.add(start_node)

    while queue:
        # Dequeue a vertex from queue
        vertex = queue.popleft()
        # print(str(vertex) + " ", end="")

        # Enqueue all neighbors that have not been visited
        for neighbor in graph[vertex]:
            if neighbor not in visited:
                visited.add(neighbor)
                queue.append(neighbor)
    return visited


def dfs(graph, start):
    visited = set()
    stack = [start]

    while stack:
        # Pop the top vertex from stack
        vertex = stack.pop()

        if vertex not in visited:
            visited.add(vertex)
            # print(str(vertex) + " ", end="")
            # Push all unvisited neighbors onto the stack
            for neighbor in graph[vertex]:
                if neighbor not in visited:
                    stack.append(neighbor)
    return visited


def measure_time(func, graph, start):
    import time
    start_time = time.time()
    func(graph, start)
    end_time = time.time()
    return end_time - start_time


def print_results(func, arr, times):
    print("{:<0} {:>7}".format('Vertexes', 'Time'))
    for i in arr:
        print("{:<6d} {:>12.5f}".format(
            i, times[arr.index(i)]))


def plot_results(x, y):
    import matplotlib.pyplot as plt
    plt.plot(x, y)
    plt.xlabel("Vertexes")
    plt.ylabel("Time in Seconds")


if __name__ == '__main__':
    import matplotlib.pyplot as plt
    n = []
    bfs_times = []
    dfs_times = []
    for i in range(500, 7001, 500):
        n.append(i)
        G = generate_random_graph(i)
        bfs_times.append(measure_time(bfs, G, 0))
        dfs_times.append(measure_time(dfs, G, 0))

    print_results(bfs, n, bfs_times)
    print_results(dfs, n, dfs_times)
    plot_results(n, bfs_times)
    plt.show()
    plot_results(n, dfs_times)
    plt.show()
    plot_results(n, bfs_times)
    plot_results(n, dfs_times)
    plt.show()
