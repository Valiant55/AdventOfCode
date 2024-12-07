def parse(file_name: str) -> tuple[list[int], list[int]]:
    with open(file_name, "r") as file:
        contents = file.readlines()

    left = []
    right = []

    for line in contents:
        first, second = line.split()
        left.append(int(first))
        right.append(int(second))

    return (left, right)


def part1(file_name: str) -> None:
    left, right = parse(file_name)
    dist = []

    left.sort()
    right.sort()

    for l, r in zip(left, right):
        d = abs(l - r)
        dist.append(d)

    answer = sum(dist)
    print(f"Part 1 solution is: {answer}")

def part2(file_name: str) -> None:
    left, right = parse(file_name)
    scores = []

    counts = {}
    for num in right:
        if counts.get(num) is None:
            counts[num] = 1
        else:
            counts[num]+=1

    for num in left:
        multiplier = counts.get(num, 0)
        scores.append(num * multiplier)

    answer = sum(scores)
    print(f"Part 2 solution is: {answer}")

if __name__ == "__main__":
    part1("input/example.txt")
    part1("input/input.txt")
    part2("input/example.txt")
    part2("input/input.txt")
    