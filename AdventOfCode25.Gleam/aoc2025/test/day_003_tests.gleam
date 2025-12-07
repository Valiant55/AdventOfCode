import files/read
import solutions/day_003

pub fn part01_example_test() {
  let input =
    read.read_file(
      "C:\\Users\\Matt\\Documents\\VS Code\\AdventOfCode\\AdventOfCode25.Gleam\\aoc2025\\src\\inputs\\day_003_example.txt",
      "\r\n",
    )

  let first = day_003.first(input)
  let second = day_003.second(input)

  assert first == 357
  assert second == 3_121_910_778_619
}
