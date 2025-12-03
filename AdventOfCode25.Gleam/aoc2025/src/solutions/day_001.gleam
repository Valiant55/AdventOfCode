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
  let new_position = position(start, rotation)

  case new_position % 100 {
    pos if pos < 0 -> 100 + pos
    pos if pos > 0 -> pos
    _ -> 0
  }
}

fn position(start: Int, rotation: Rotation) -> Int {
  case rotation.direction {
    Left -> start - rotation.magnitude
    Right -> start + rotation.magnitude
  }
}

pub fn second(inputs: List(String)) -> Int {
  inputs
  |> list.map(create_rotation)
  |> list.scan(RotationResult(0, 50), rotate_clicks)
  |> list.last()
  |> result.unwrap(RotationResult(0, 50))
  |> fn(r) { r.total_clicks }
}

pub fn rotate_clicks(
  start: RotationResult,
  rotation: Rotation,
) -> RotationResult {
  let new_position = rotate(start.position, rotation)
  let raw_position = position(start.position, rotation)

  let clicks =
    0
    |> offset_rots(rotation.direction, raw_position, start.position)
    |> count_rots(raw_position)

  RotationResult(start.total_clicks + clicks, new_position)
}

fn offset_rots(
  clicks: Int,
  direction: Direction,
  raw_position: Int,
  start: Int,
) -> Int {
  case raw_position, direction {
    pos, Left if pos <= 0 && start != 0 -> clicks + 1
    _, _ -> clicks
  }
}

fn count_rots(clicks: Int, raw_position: Int) -> Int {
  clicks + int.absolute_value(raw_position) / 100
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
