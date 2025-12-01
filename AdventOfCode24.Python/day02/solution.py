import os

def parse(file_name: str) -> list[str]:
    with open(file_name, "r") as file:
        return file.readlines()

def part1(file_name: str) -> None:
    reports = parse(file_name)
    safeCount = 0

    for report in reports:
        levels = [int(r) for r in report.split()]

        if len(levels) < 1:
            # No readings, unsafe
            continue

        if len(levels) < 2:
            # Only one reading, safe
            safeCount+=1
            continue
        
        if levels[0] < levels[1]:
            # Increasing
            minLevel = 1
            maxLevel = 3
        else:
            # Decreasing
            minLevel = -1
            maxLevel = -3

        safe = True
        for curr, nxt in zip(levels, levels[1:]):
            diff = curr - nxt
            if not minLevel <= diff <= maxLevel:
                safe = False
                break

        if safe:
            safeCount+=1

    print(f"Part 1 solution is: {safeCount}")

def part2(file_name: str) -> None:
    reports = parse(file_name)
    print(f"Part 2 solution is: {answer}")

if __name__ == "__main__":
    cwd = os.getcwd()

    part1(f"{cwd}/input/example.txt")
    #part1("input/input.txt")
    #part2("input/example.txt")
    #part2("input/input.txt")
    