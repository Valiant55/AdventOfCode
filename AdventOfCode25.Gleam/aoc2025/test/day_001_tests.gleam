import files/read
import solutions/day_001

pub fn example_test() {
  let input =
    read.read_file(
      "C:\\Users\\Matt\\Documents\\VS Code\\AdventOfCode\\AdventOfCode25.Gleam\\aoc2025\\src\\inputs\\day_001_example.txt",
      "\r\n",
    )

  let first = day_001.first(input)
  let second = day_001.second(input)

  assert first == 3
  assert second == 6
}

pub fn example2_test() {
  let input =
    read.read_file(
      "C:\\Users\\Matt\\Documents\\VS Code\\AdventOfCode\\AdventOfCode25.Gleam\\aoc2025\\src\\inputs\\day_001_example2.txt",
      "\r\n",
    )

  let first = day_001.first(input)
  let second = day_001.second(input)

  assert first == 6
  assert second == 27
}

pub fn rotate_clicks_example_test() {
  let res = day_001.RotationResult(0, 50)
  let rot = day_001.Rotation(day_001.Right, 1000)

  let sut = day_001.rotate_clicks(res, rot)

  assert sut.position == 50
  assert sut.total_clicks == 10
}

pub fn rotate_clicks_left01_test() {
  let res = day_001.RotationResult(0, 0)
  let rot = day_001.Rotation(day_001.Left, 1)

  let sut = day_001.rotate_clicks(res, rot)

  assert sut.position == 99
  assert sut.total_clicks == 0
}

pub fn rotate_clicks_left100_test() {
  let res = day_001.RotationResult(0, 0)
  let rot = day_001.Rotation(day_001.Left, 100)

  let sut = day_001.rotate_clicks(res, rot)

  assert sut.position == 0
  assert sut.total_clicks == 1
}

pub fn rotate_clicks_left150_test() {
  let res = day_001.RotationResult(0, 0)
  let rot = day_001.Rotation(day_001.Left, 150)

  let sut = day_001.rotate_clicks(res, rot)

  assert sut.position == 50
  assert sut.total_clicks == 1
}

pub fn rotate_clicks_right01_test() {
  let res = day_001.RotationResult(0, 0)
  let rot = day_001.Rotation(day_001.Right, 1)

  let sut = day_001.rotate_clicks(res, rot)

  assert sut.position == 1
  assert sut.total_clicks == 0
}

pub fn rotate_clicks_right100_test() {
  let res = day_001.RotationResult(0, 0)
  let rot = day_001.Rotation(day_001.Right, 100)

  let sut = day_001.rotate_clicks(res, rot)

  assert sut.position == 0
  assert sut.total_clicks == 1
}

pub fn rotate_clicks_right150_test() {
  let res = day_001.RotationResult(0, 99)
  let rot = day_001.Rotation(day_001.Right, 150)

  let sut = day_001.rotate_clicks(res, rot)

  assert sut.position == 49
  assert sut.total_clicks == 2
}

pub fn rotate_clicks_right101_test() {
  let res = day_001.RotationResult(0, 0)
  let rot = day_001.Rotation(day_001.Right, 101)

  let sut = day_001.rotate_clicks(res, rot)

  assert sut.position == 1
  assert sut.total_clicks == 1
}

pub fn rotate_clicks_right200_test() {
  let res = day_001.RotationResult(0, 0)
  let rot = day_001.Rotation(day_001.Right, 200)

  let sut = day_001.rotate_clicks(res, rot)

  assert sut.position == 0
  assert sut.total_clicks == 2
}

pub fn rotate_clicks_right201_test() {
  let res = day_001.RotationResult(0, 0)
  let rot = day_001.Rotation(day_001.Right, 201)

  let sut = day_001.rotate_clicks(res, rot)

  assert sut.position == 1
  assert sut.total_clicks == 2
}
