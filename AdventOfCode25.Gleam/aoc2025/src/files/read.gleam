import gleam/string
import simplifile

pub fn read_file(file_path: String) -> List(String) {
  simplifile.read(file_path)
  |> split_lines
}

pub fn split_lines(input: Result(String, simplifile.FileError)) -> List(String) {
  case input {
    Ok(str) -> string.split(str, "\r\n")
    _ -> []
  }
}
