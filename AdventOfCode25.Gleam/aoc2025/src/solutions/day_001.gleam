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
  RotationResult(total_clicks: Int, new_position: Int)
}

pub fn rotate(start: Int, rotation: Rotation) -> Int {
  let new_position = case rotation.direction {
    Left -> start - rotation.magnitude
    Right -> start + rotation.magnitude
  }

  let result = case new_position % 100 {
    pos if pos < 0 -> 100 + pos
    pos if pos > 0 -> pos
    _ -> 0
  }

  result
}

pub fn rotate_clicks(
  start: RotationResult,
  rotation: Rotation,
) -> RotationResult {
  let rotation_result = case rotation.direction {
    Left -> start.new_position - rotation.magnitude
    Right -> start.new_position + rotation.magnitude
  }

  let new_position = case rotation_result % 100 {
    pos if pos < 0 -> 100 + pos
    pos if pos > 0 -> pos
    _ -> 0
  }

  //echo rotation_result

  let clicks =
    rotation.magnitude
    |> int.divide(100)
    |> result.unwrap(0)
    |> int.absolute_value
    |> count_position_zero(start.new_position, new_position)

  let result = RotationResult(start.total_clicks + clicks, new_position)
  echo result
  result
}

pub fn count_position_zero(
  clicks: Int,
  start_position: Int,
  new_position: Int,
) -> Int {
  case start_position, new_position {
    start, end if start != 0 && end == 0 -> clicks + 1
    _, _ -> clicks
  }
}

pub fn first(inputs: List(String)) -> Int {
  inputs
  |> list.map(create_rotation)
  |> list.scan(50, rotate)
  |> list.filter(fn(i) { i == 0 })
  |> list.length()
}

pub fn second(inputs: List(String)) -> Int {
  inputs
  |> list.map(create_rotation)
  |> list.scan(RotationResult(0, 50), rotate_clicks)
  |> list.last()
  |> result.unwrap(RotationResult(0, 50))
  |> fn(r) { r.total_clicks }
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
