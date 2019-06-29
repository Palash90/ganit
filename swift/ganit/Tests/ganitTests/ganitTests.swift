import XCTest
@testable import ganit

final class ganitTests: XCTestCase {
    func testExample() {
        // This is an example of a functional test case.
        // Use XCTAssert and related functions to verify your tests produce the correct
        // results.
        XCTAssertEqual(ganit().text, "Hello, World!")
    }

    static var allTests = [
        ("testExample", testExample),
    ]
}
