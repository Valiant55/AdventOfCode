import gleam/string
import simplifile

pub fn read_file(file_path: String, delimiter: String) -> List(String) {
  simplifile.read(file_path)
  |> split_lines(delimiter)
}

pub fn split_lines(
  input: Result(String, simplifile.FileError),
  delimiter: String,
) -> List(String) {
  case input {
    Ok(str) -> string.split(str, delimiter)
    _ -> []
  }
}
