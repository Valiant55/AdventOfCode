import gleam/int
import gleam/list
import gleam/result
import gleam/string

pub type Battery {
  Battery(joltage: Int, position: Int)
}

pub fn first(input: List(String)) -> Int {
  let len =
    input
    |> list.first
    |> result.unwrap("")
    |> string.length

  let first_max =
    input
    |> list.map(parse_line)
    |> list.map(find_max(_, list.take(_, len - 1)))

  let second_max =
    input
    |> list.map(parse_line)
    |> list.zip(first_max)
    |> list.map(fn(tup) {
      find_max(tup.0, list.drop(_, { tup.1 }.position + 1))
    })

  first_max
  |> list.zip(second_max)
  |> list.map(joltage)
  |> list.fold(0, fn(a, i) { a + i })
}

pub fn second(input: List(String)) -> Int {
  todo
}

fn parse_line(input: String) -> List(Int) {
  input
  |> string.to_graphemes
  |> list.map(int.parse)
  |> list.map(result.unwrap(_, 0))
}

fn find_max(input: List(Int), filter: fn(List(Int)) -> List(Int)) -> Battery {
  input
  |> filter()
  |> list.zip(list.range(0, 1000))
  |> list.max(fn(tup1, tup2) { int.compare(tup1.0, tup2.0) })
  |> result.unwrap(#(0, 0))
  |> fn(tup) { Battery(tup.0, tup.1) }
}

fn joltage(tuple: #(Battery, Battery)) -> Int {
  { tuple.0 }.joltage * 10 + { tuple.1 }.joltage
}
