use std::string::String;
use std::vec::Vec;

use crate::parser::token::Token;

mod token;

pub fn tokenize(input: String) -> Vec<Token> {
    let chars = input.as_bytes();
    let _length = chars.len();
    let mut _index: usize = 0;
    let mut _line_number = 0;
    let mut _column_number = 0;
    let mut str_literal_value: String = String::from("");
    let mut tokens: Vec<Token> = Vec::new();


    while _index < _length {
        let current_char = chars[_index] as char;

      //  println!("{} {} {} {}", _line_number, _column_number, current_char, &str_literal_value);

        if current_char == '\n' {
            _line_number = _line_number + 1;
            _column_number = 0;
            str_literal_value = String::from("");
        } else if current_char == ' ' {
            let token: Token = Token {
                line: _line_number,
                column: _column_number,
                token_type: "int".to_string(),
                str_value: str_literal_value,
                double_value: 0.0,
                int_value: 0,
                bool_value: false,
            };
            tokens.push(token);
        }
        _index = _index + 1;
        _column_number = _column_number + 1;
    }
    return tokens;
}