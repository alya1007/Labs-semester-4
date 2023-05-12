import collections
import networkx as nx
import random
from queue import PriorityQueue
from typing import List


def generate_random_graph(num_nodes):
    G = nx.Graph()
    for i in range(num_nodes):
        G.add_node(i)
    for i in range(num_nodes):
        for j in range(i + 1, num_nodes):
            if random.random() < 0.5:  # Add an edge with probability 0.5
                weight = random.randint(1, 100)  # Generate a random weight
                G.add_weighted_edges_from([(i, j, weight)])
    return G


def kruskal(G: nx.Graph) -> List[nx.Graph]:
    mst = []
    sets = [{u} for u in G.nodes()]
    edges = list(G.edges(data="weight"))
    edges.sort(key=lambda x: x[2])
    for u, v, w in edges:
        set_u = None
        set_v = None
        for s in sets:
            if u in s:
                set_u = s
            if v in s:
                set_v = s
        if set_u != set_v:
            mst.append((u, v))
            set_u |= set_v
            sets.remove(set_v)
        if len(mst) == len(G) - 1:
            break
    return [G.edge_subgraph(mst)]


def prim(graph: nx.Graph) -> nx.Graph:
    T = nx.Graph()
    T.add_node(0)
    while len(T) < len(graph):
        min_edge = None
        min_weight = float("inf")
        for u in T.nodes():
            for v in graph.neighbors(u):
                if v not in T:
                    weight = graph.edges[u, v]["weight"]
                    if weight < min_weight:
                        min_edge = (u, v)
                        min_weight = weight
        T.add_node(min_edge[1])
        T.add_edge(*min_edge, weight=min_weight)


def measure_time(func, graph):
    import time

    start_time = time.time()
    func(graph)
    end_time = time.time()
    return end_time - start_time


def print_results(func, arr, times):
    print("{:<0} {:>7}".format("Vertexes", "Time"))
    for i in arr:
        print("{:<6d} {:>12.5f}".format(i, times[arr.index(i)]))


def plot_results(x, y):
    import matplotlib.pyplot as plt

    plt.plot(x, y)
    plt.xlabel("Vertexes")
    plt.ylabel("Time in Seconds")


if __name__ == "__main__":
    import matplotlib.pyplot as plt

    n = []
    kruskal_times = []
    prim_times = []
    for i in range(100, 301, 100):
        n.append(i)
        G = generate_random_graph(i)
        kruskal_times.append(measure_time(kruskal, G))
        prim_times.append(measure_time(prim, G))

    print_results(kruskal, n, kruskal_times)
    print_results(prim, n, prim_times)
    plot_results(n, kruskal_times)
    plt.show()
    plot_results(n, prim_times)
    plt.show()
    plot_results(n, kruskal_times)
    plot_results(n, prim_times)
    plt.show()
