import gleam/int
import gleam/list
import gleam/result
import gleam/string

pub fn first(input: List(String)) -> Int {
  input
  |> list.map(split_range)
  |> list.map(invalid_ids(_, is_repeated))
  |> list.flatten
  |> list.fold(0, fn(a, i) { a + i })
}

pub fn second(input: List(String)) -> Int {
  input
  |> list.map(split_range)
  |> list.map(invalid_ids(_, has_repeating_str))
  |> list.flatten
  |> list.fold(0, fn(a, i) { a + i })
}

fn split_range(range: String) -> #(Int, Int) {
  string.split(range, "-")
  |> list.map(int.parse)
  |> list.map(fn(i) { result.unwrap(i, 0) })
  |> fn(l) {
    case l {
      [first, second] -> #(first, second)
      _ -> panic
    }
  }
}

fn invalid_ids(range: #(Int, Int), is_valid: fn(Int) -> Bool) -> List(Int) {
  list.range(range.0, range.1)
  |> list.filter(is_valid)
}

fn is_repeated(id: Int) -> Bool {
  let str = int.to_string(id)
  let len = string.length(str)

  let first = string.slice(str, 0, len / 2)
  let second = string.slice(str, len / 2, len / 2)

  int.is_even(len) && first == second
}

fn has_repeating_str(id: Int) -> Bool {
  let str = int.to_string(id)
  let len = string.length(str)

  list.range(1, len / 2)
  |> list.map(string.slice(str, 0, _))
  |> list.map(fn(s) { string.repeat(s, len / string.length(s)) })
  |> list.any(fn(s) { len != 1 && s == str })
}
