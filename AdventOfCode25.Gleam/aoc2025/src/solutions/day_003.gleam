import gleam/int
import gleam/list
import gleam/order
import gleam/result
import gleam/string

pub type Battery {
  Battery(joltage: Int, position: Int)
}

pub fn first(input: List(String)) -> Int {
  let len = get_max(input)

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
  |> int.sum
}

pub fn second(input: List(String)) -> Int {
  let len = get_max(input)
  let first_max = len - 12

  let first_num =
    input
    |> list.map(parse_line)
    |> list.map(find_max(_, list.take(_, first_max)))

  input
  |> list.map(parse_line)
  |> list.map(to_batteries)
  |> list.zip(first_num)
  |> list.map(fn(tuple) {
    tuple.0
    |> list.drop({ tuple.1 }.position + 1)
    |> list.sort(order.reverse(by_joltage))
    |> list.take(11)
    |> list.prepend(tuple.1)
  })
  |> list.map(calculate_joltage)
  |> int.sum
}

fn parse_line(input: String) -> List(Int) {
  input
  |> string.to_graphemes
  |> list.map(int.parse)
  |> list.map(result.unwrap(_, 0))
}

fn get_max(input: List(String)) -> Int {
  input
  |> list.first
  |> result.unwrap("")
  |> string.length
}

fn find_max(input: List(Int), filter: fn(List(Int)) -> List(Int)) -> Battery {
  input
  |> filter()
  |> to_batteries
  |> list.sort(by_joltage_desc_and_position)
  |> list.first
  |> result.unwrap(Battery(0, -1))
}

fn to_batteries(input: List(Int)) -> List(Battery) {
  input
  |> list.zip(list.range(0, 1000))
  |> list.map(to_battery)
}

fn to_battery(tuple: #(Int, Int)) {
  Battery(tuple.0, tuple.1)
}

fn by_joltage(b1: Battery, b2: Battery) -> order.Order {
  case b1.joltage, b2.joltage {
    j1, j2 if j1 == j2 -> order.Eq
    j1, j2 if j1 > j2 -> order.Gt
    j1, j2 if j1 < j2 -> order.Lt
    _, _ -> panic as "Invalid battery comparison"
  }
}

fn by_position(b1: Battery, b2: Battery) -> order.Order {
  case b1.position, b2.position {
    j1, j2 if j1 == j2 -> order.Eq
    j1, j2 if j1 > j2 -> order.Gt
    j1, j2 if j1 < j2 -> order.Lt
    _, _ -> panic as "Invalid battery comparison"
  }
}

fn by_joltage_and_position(b1: Battery, b2: Battery) -> order.Order {
  order.break_tie(by_joltage(b1, b2), by_position(b1, b2))
}

fn by_joltage_desc_and_position(b1: Battery, b2: Battery) -> order.Order {
  order.break_tie(order.reverse(by_joltage)(b1, b2), by_position(b1, b2))
}

fn joltage(tuple: #(Battery, Battery)) -> Int {
  { tuple.0 }.joltage * 10 + { tuple.1 }.joltage
}

fn calculate_joltage(input: List(Battery)) -> Int {
  input
  |> list.sort(by_position)
  |> list.reverse
  |> list.zip(
    list.range(0, 12)
    |> list.map(fn(i) { power(10, i) }),
  )
  |> list.map(fn(tup) { { tup.0 }.joltage * tup.1 })
  |> int.sum
}

fn power(base: Int, exponent: Int) -> Int {
  base
  |> list.repeat(exponent)
  |> list.fold(1, fn(a, i) { a * i })
}
