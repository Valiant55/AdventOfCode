import files/read
import solutions/day_002

pub fn part01_example_test() {
  let input =
    read.read_file(
      "C:\\Users\\Matt\\Documents\\VS Code\\AdventOfCode\\AdventOfCode25.Gleam\\aoc2025\\src\\inputs\\day_002_example.txt",
      ",",
    )

  let first = day_002.first(input)
  let second = day_002.second(input)

  assert first == 1_227_775_554
  assert second == 4_174_379_265
}
