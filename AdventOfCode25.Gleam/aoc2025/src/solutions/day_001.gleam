import gleam/int
import gleam/list
import gleam/result
import gleam/string
import simplifile

pub type Direction {
  Left
  Right
}

pub type Rotation {
  Rotation(direction: Direction, magnitude: Int)
}

pub type RotationResult {
  RotationResult(total_clicks: Int, new_position: Int)
}

pub type ParseError {
  UnsupportedDirection
  InvalidMagnitude
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

pub fn get_input(filepath: String) -> List(Rotation) {
  simplifile.read(filepath)
  |> split_lines
  |> list.map(create_rotation)
  |> list.map(to_list)
  |> list.flatten
}

pub fn first(inputs: List(Rotation)) -> Int {
  inputs
  |> list.scan(50, rotate)
  |> list.filter(fn(i) { i == 0 })
  |> list.length()
}

pub fn second(inputs: List(Rotation)) -> Int {
  inputs
  |> list.scan(RotationResult(0, 50), rotate_clicks)
  |> list.last()
  |> result.unwrap(RotationResult(0, 50))
  |> fn(r) { r.total_clicks }
}

fn split_lines(input: Result(String, simplifile.FileError)) -> List(String) {
  case input {
    Ok(str) -> string.split(str, "\r\n")
    _ -> []
  }
}

fn create_rotation(input: String) -> Result(Rotation, ParseError) {
  let direction = string.first(input)
  let mag =
    input
    |> string.drop_start(1)
    |> int.parse

  case direction, mag {
    Ok("L"), Ok(mag) -> Ok(Rotation(Left, mag))
    Ok("R"), Ok(mag) -> Ok(Rotation(Right, mag))
    _, Error(Nil) -> Error(InvalidMagnitude)
    _, _ -> Error(UnsupportedDirection)
  }
}

fn to_list(rotation: Result(Rotation, ParseError)) -> List(Rotation) {
  case rotation {
    Ok(rot) -> [rot]
    _ -> []
  }
}
