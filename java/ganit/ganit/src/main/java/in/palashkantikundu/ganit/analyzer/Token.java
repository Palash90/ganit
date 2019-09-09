/**
 *
 */
package in.palashkantikundu.ganit.analyzer;

public class Token {
	private Object value;
	private TokenType type;
	private int lineNumber;
	private int columnNumber;

	public Token(Object tokenValue, TokenType type, int lineNumber, int columnNumber) {
		this.value = tokenValue;
		this.type = type;
		this.lineNumber = lineNumber;
		this.columnNumber = columnNumber;

	}

	public Object getValue() {
		return value;
	}

	public TokenType getType() {
		return type;
	}

	public int getLineNumber() {
		return lineNumber;
	}

	public int getColumnNumber() {
		return columnNumber;
	}

	@Override
	public String toString() {
		return "[" + this.value + ", " + this.type + ", " + this.lineNumber + ", " + this.columnNumber + "]";
	}

}
