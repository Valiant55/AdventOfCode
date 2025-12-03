import gleam/int
import gleam/io
import gleam/list
import gleam/result
import gleam/string

pub type Direction {
  Left
  Right
}

pub type Rotation {
  Rotation(direction: Direction, magnitude: Int)
  InvalidRotation(direction: Direction, magnitude: Int)
}

pub type RotationResult {
  RotationResult(total_clicks: Int, position: Int)
}

pub fn first(inputs: List(String)) -> Int {
  inputs
  |> list.map(create_rotation)
  |> list.scan(50, rotate)
  |> list.filter(fn(i) { i == 0 })
  |> list.length()
}

pub fn rotate(start: Int, rotation: Rotation) -> Int {
  let new_position = case rotation.direction {
    Left -> start - rotation.magnitude
    Right -> start + rotation.magnitude
  }

  case new_position % 100 {
    pos if pos < 0 -> 100 + pos
    pos if pos > 0 -> pos
    _ -> 0
  }
}

pub fn second(inputs: List(String)) -> Int {
  inputs
  |> list.map(create_rotation)
  |> list.scan(RotationResult(0, 50), rotate_clicks)
  |> echo
  |> list.last()
  |> result.unwrap(RotationResult(0, 50))
  |> fn(r) { r.total_clicks }
}

pub fn rotate_clicks(
  start: RotationResult,
  rotation: Rotation,
) -> RotationResult {
  let new_position = rotate(start.position, rotation)

  let clicks = 0

  let result = RotationResult(start.total_clicks + clicks, new_position)
  result
}

fn create_rotation(line: String) -> Rotation {
  let direction = string.first(line)
  let mag =
    line
    |> string.drop_start(1)
    |> int.parse

  case direction, mag {
    Ok("L"), Ok(mag) -> Rotation(Left, mag)
    Ok("R"), Ok(mag) -> Rotation(Right, mag)
    _, _ -> {
      io.print_error("Invalid data found: " <> line)
      panic
    }
  }
}
