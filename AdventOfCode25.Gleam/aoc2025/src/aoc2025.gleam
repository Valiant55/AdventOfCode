import files/read
import gleam/int
import gleam/io
import solutions/day_002

pub fn main() -> Nil {
  let input =
    read.read_file(
      "C:\\Users\\Matt\\Documents\\VS Code\\AdventOfCode\\AdventOfCode25.Gleam\\aoc2025\\src\\inputs\\day_002.txt",
      ",",
    )

  let answer = day_002.second(input)

  io.print("The answer is: " <> int.to_string(answer))

  Nil
}
