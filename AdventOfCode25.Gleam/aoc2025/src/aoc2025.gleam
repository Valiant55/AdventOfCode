import gleam/int
import gleam/io
import solutions/day_001

pub fn main() -> Nil {
  let input =
    day_001.get_input(
      "C:\\Users\\Matt\\Documents\\VS Code\\AdventOfCode\\AdventOfCode25.Gleam\\aoc2025\\src\\inputs\\day_001.txt",
    )

  let answer = day_001.second(input)

  io.print("The answer is: " <> int.to_string(answer))

  Nil
}
