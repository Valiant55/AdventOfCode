import files/read
import gleam/int
import gleam/io
import solutions/day_003

pub fn main() -> Nil {
  let input =
    read.read_file(
      "C:\\Users\\Matt\\Documents\\VS Code\\AdventOfCode\\AdventOfCode25.Gleam\\aoc2025\\src\\inputs\\day_003.txt",
      "\r\n",
    )

  let answer = day_003.first(input)

  io.print("The answer is: " <> int.to_string(answer))

  Nil
}
