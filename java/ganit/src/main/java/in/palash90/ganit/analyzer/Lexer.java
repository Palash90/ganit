/**
 *
 */
package in.palash90.ganit.analyzer;

import java.util.ArrayList;
import java.util.List;

/**
 * @author Palash Kanti Kundu
 *
 */
public class Lexer {
	private int position;
	private int length;
	private char[] inputChars;
	private String tokenTerminators;

	public Lexer(String input) {

		this.tokenTerminators = " +-*/%;}\n\0";
		input += '\0';
		this.position = 0;
		this.length = input.length();
		this.inputChars = input.toCharArray();
	}

	private Token getToken(String currentToken, int lineNumber, int columnNumber) {
		TokenType type = TokenType.STRING;
		Object tokenValue = currentToken;
		switch (currentToken) {

		case "+":
			type = TokenType.PLUS;
			break;
		case "-":
			type = TokenType.MINUS;
			break;
		case "*":
			type = TokenType.MULTIPLY;
			break;
		case "/":
			type = TokenType.DIVIDE;
			break;
		case "%":
			type = TokenType.MODULO;
			break;
		case ";":
			type = TokenType.SEMI;
			break;

		}

		try {
			tokenValue = Double.parseDouble(currentToken);
			type = TokenType.NUMBER;

		} catch (NumberFormatException e) {

		}

		Token token = new Token(tokenValue, type, lineNumber, columnNumber);
		return token;

	}

	public List<Token> lex() {
		int lineNumber = 1;
		int columnNumber = 0;
		int tokenStartPosition = 1;

		String currentToken = "";

		List<Token> tokens = new ArrayList<Token>();

		while (position < length) {

			System.out.println("Encountered " + inputChars[position]);

			if (inputChars[position] == '\n') {
				System.out.println(inputChars);
				System.out.println("New line at position " + position);
				lineNumber += 1;
				columnNumber = 0;

			}
			columnNumber++;

			if (tokenTerminators.indexOf(inputChars[position]) != -1) {
				addToken(lineNumber, tokenStartPosition, tokens, currentToken);

				if (inputChars[position] != ' ') {
					String operatorValue = Character.toString(inputChars[position]).trim();
					addToken(lineNumber, columnNumber, tokens, operatorValue);
				}

				currentToken = "";
				tokenStartPosition = columnNumber + 1;

			} else {
				currentToken += inputChars[position];

			}

			position++;

		}

		return tokens;

	}

	private void addToken(int lineNumber, int columnNumber, List<Token> tokens, String operatorValue) {
		if (operatorValue.length() > 0) {
			tokens.add(getToken(operatorValue, lineNumber, columnNumber));
		}
	}

}
