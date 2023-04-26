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
                weight = random.randint(1, 100)  # Generate a random weight
                G.add_weighted_edges_from([(i, j, weight)])
    return G


def dijkstra(graph, start):
    import heapq
    """
    Apply Dijkstra's algorithm to a networkx graph starting from a given node.
    Args:
        graph (networkx.Graph): The graph to apply Dijkstra's algorithm to.
        start: The starting node.
    Returns:
        dict: A dictionary where the keys are the nodes in the graph and the values are the shortest distances
        from the start node to each node.
    """
    distances = {node: float('inf') for node in graph.nodes(
    )}  # Initialize all distances as infinite
    distances[start] = 0  # Distance from start node to itself is 0
    # Priority queue of (distance, node) tuples, starting with the start node
    heap = [(0, start)]
    visited = set()  # Set of visited nodes

    while heap:
        # Get the node with the smallest distance
        (dist, current_node) = heapq.heappop(heap)
        if current_node in visited:
            continue  # Ignore nodes that have already been visited
        visited.add(current_node)
        for neighbor in graph.neighbors(current_node):
            tentative_distance = dist + graph[current_node][neighbor]['weight']
            if tentative_distance < distances[neighbor]:
                distances[neighbor] = tentative_distance
                # Add the neighbor to the queue
                heapq.heappush(heap, (tentative_distance, neighbor))

    return distances


def floyd_warshall(graph, start):
    import numpy as np
    """
    Applies the Floyd-Warshall algorithm to the given graph.
    Args:
        graph (networkx.Graph): The graph to apply the algorithm to.
    Returns:
        numpy.ndarray: A 2D array where element i,j is the shortest path from node i to node j in the graph.
    """
    nodes = list(graph.nodes())
    dist = np.full((len(nodes), len(nodes)), np.inf)
    np.fill_diagonal(dist, 0)

    for node1, node2, weight in graph.edges(data='weight'):
        dist[node1-1, node2-1] = weight
        dist[node2-1, node1-1] = weight

    for k in range(len(nodes)):
        for i in range(len(nodes)):
            for j in range(len(nodes)):
                dist[i, j] = min(dist[i, j], dist[i, k] + dist[k, j])

    return dist


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
    dijkstra_times = []
    floyd_warshall_times = []
    for i in range(100, 701, 100):
        n.append(i)
        G = generate_random_graph(i)
        dijkstra_times.append(measure_time(dijkstra, G, 0))
        floyd_warshall_times.append(measure_time(floyd_warshall, G, 0))

    print_results(dijkstra, n, dijkstra_times)
    print_results(floyd_warshall, n, floyd_warshall_times)
    plot_results(n, dijkstra_times)
    plt.show()
    plot_results(n, floyd_warshall_times)
    plt.show()
    plot_results(n, dijkstra_times)
    plot_results(n, floyd_warshall_times)
    plt.show()
