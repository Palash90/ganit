pub struct Token {
    pub line: usize,
    pub column: usize,
    pub token_type: String,
    pub int_value: u64,
    pub double_value: f64,
    pub str_value: String,
    pub bool_value: bool,
}

impl Token {
    fn to_string(&self) -> String {
        let mut str = format!("Line: {};", self.line + 1);
        str = format!("{} Column: {};", str.to_string(), self.column + 1);
        str = format!("{} Token Type: {};", str.to_string(), self.token_type);

        if self.token_type == "int".to_string() {
            str = format!("{} value: {}", str.to_string(), self.int_value);
        } else if self.token_type == "double".to_string() {
            str = format!("{} value: {}", str.to_string(), self.double_value);
        } else if self.token_type == "str".to_string() {
            str = format!("{} value: {}", str.to_string(), self.str_value);
        } else if self.token_type == "operator".to_string() {
            str = format!("{} value: {}", str.to_string(), self.str_value);
        }

        return str;
    }
}

impl std::fmt::Display for Token {
    fn fmt(&self, f: &mut std::fmt::Formatter) -> std::fmt::Result {
        write!(f, "{}", self.to_string())
    }
}

impl std::fmt::Debug for Token {
    fn fmt(&self, f: &mut std::fmt::Formatter) -> std::fmt::Result {
        write!(f, "{}", self.to_string())
    }
}