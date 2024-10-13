Project: Magento Selenium Automation Demo (using my Selenium/Allure Nugget Package)
This project is a Selenium-based automation test suite written in C# targeting a Magento demo website. The primary purpose of the project is to verify the functionality and UI elements of various pages on the Magento site, including login, registration, and navigation. The suite also includes configurations for caching and Allure reports integration to ensure detailed logging and tracking of test results.

##You-tube links
##smoke test:
https://youtu.be/qw1nb3ODEtA?si=mlwi9QaDhs5Sz6T7
##allure report:
https://www.youtube.com/watch?v=QNwfTVNXhIA

## Project Structure
```bash
MagentoSelenuimAutomationDemo/
├── Objects/
│   └── UserMail.cs                     # Class handling user email details.
├── Tests/
│   └── MagentoSmokeTest.cs             # Core test file containing smoke tests.
│   └── /PageFactories
│       ├── LogInPage.cs                # Page object for handling login actions and elements.
│       ├── MegentoHomePage.cs          # Page object for handling the main homepage elements.
│       └── RegisterPage.cs             # Page object for handling user registration actions.
├── Utils/
│   └── /Caching
│       └── CachingHelper.cs            # Utility class for managing caching operations.
├── appsettings.json                    # Configuration file for general test settings and credentials.
├── cachingData.json                    # Additional data for managing cached information.
└── ReadMe.txt                          # Project documentation and guidelines.
```

Detailed Breakdown:
MagentoSmokeTest.cs: This class contains five primary smoke tests, each focusing on different aspects of the Magento demo site. The tests use Allure for reporting, capturing detailed logs and screenshots on failure. The tests include:

Test1: Verifies that the homepage header, body, and footer elements are visible.
Test2: Tests the functionality of header navigation links by comparing URL responses.
Test3: Performs a complete registration and verifies if the user details match.
Test4: Executes login validation for registered users.
Test5: Logs out a logged-in user and checks for a successful logout message.
Page Factory Classes (LogInPage.cs, MegentoHomePage.cs, and RegisterPage.cs): These classes use the Page Object Model (POM) pattern, encapsulating page-specific elements and their interactions. This structure simplifies test maintenance and improves readability.

LogInPage.cs: Contains elements and methods related to the login process.
MegentoHomePage.cs: Manages interactions on the Magento homepage.
RegisterPage.cs: Handles user registration elements such as name, email, and password fields.
(email with each new registration will fill a new email address from cache)
CachingHelper.cs: A helper utility for managing and updating cached data such as user registration email numbers and other frequently accessed information.

Configuration Files (appsettings.json and cachingData.json): These files store configuration data, such as URLs, user credentials, and caching information. The tests retrieve and use these configurations dynamically.

Automation Flow:
Setup Phase: Initializes the WebDriver, loads configuration files, and sets up the page objects.
Execution Phase: Each test navigates to the appropriate pages, interacts with UI elements, and performs assertions.
Reporting: The project uses Allure for real-time logging and capturing screenshots during test failures. Each test step is logged, and test outcomes are marked using Allure's severity and feature annotations.
TearDown Phase: After each test, the WebDriver is closed to ensure no lingering browser instances.
Allure Reporting:
Allure is configured to generate detailed reports for each test run, capturing success, warnings, and failures. Screenshots are attached to the report for failed tests, making it easier to debug and track issues.
