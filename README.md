# ScrapMechanicLogic

## Table of Contents

- [Introduction](#introduction)
- [Features](#features)
- [Getting Started](#getting-started)
- [Usage](#usage)
- [Configuration](#configuration)
- [Contributing](#contributing)
- [License](#license)

## Introduction

Program that converts Images, Voxels and Logic programs into blueprints in Scrap Mechanic

Programmed in VS C#. 

Very modulable to add features and elements to import.

## Features
- Automatic saving of the blueprint into all of the SM users
- Fully open source
- Interface class
### Voxel Parser
- Import .vox file format from MagicaVoxel
- Use of either .vox original palette or rounding those color into SM palette
- Per color block type attribution
- Default block type selection
- Scale Slider
### Image Parser
- Import any type of image file
- Scale down slider 
- Round image colors into SM palette / Dithering
- Block type selection
- Orientation
- Scale Slider
### Logic Parser
Comming Soon

## Getting Started

The project build is standalone for Windows 64b. Only download the latest build and extract from zip to run the app.

## Usage

1. Run the application
2. Browse for file
3. Select conversion options
4. [optionnal] Write a blueprint name
9. Convert

## Contributing

We welcome contributions from the community to help improve and grow this project. Whether you're interested in fixing a bug, implementing a new feature, or simply improving documentation, your contributions are highly appreciated.

#### Reporting Bugs

If you encounter a bug or issue while using our project, please help us by reporting it. To report a bug, follow these steps:

1. **Check Existing Issues:** Before creating a new issue, please search the issues list to see if the bug has already been reported by someone else.
2. **Create a New Issue:** If the issue doesn't already exist, open a new issue. Be sure to provide a clear and detailed description of the problem. Include information about your environment (e.g., operating system, browser version) and steps to reproduce the issue, if possible.

#### Requesting Features

If you have an idea for a new feature or enhancement, we encourage you to share it with us. To request a new feature, follow these steps:

1. **Check Existing Requests:** First, search the issues list to see if someone else has already requested the feature.
2. **Create a New Feature Request:** If it hasn't been requested before, open a new issue and clearly describe the feature you have in mind. Explain why it would be valuable and how it would benefit users.

#### Code Contributions

If you're interested in contributing code to the project, please follow these guidelines:

1. **Fork the Repository:** Fork the project's repository to your GitHub account.
2. **Create a Branch:** Create a new branch in your forked repository for the specific feature or bug fix you're working on. Use a descriptive name for your branch.
3. **Make Changes:** Make your changes or additions to the codebase.
4. **Test:** Ensure that your changes work as intended and do not introduce new issues. Run any relevant tests.
5. **Commit and Push:** Commit your changes with clear and concise commit messages. Push your branch to your forked repository.
6. **Create a Pull Request:** Open a pull request from your branch to the main repository's main branch. Provide a detailed description of your changes.
7. **Code Review:** Your pull request will be reviewed by maintainers. Be prepared to make changes based on feedback.
8. **Merge:** Once your changes are approved, they will be merged into the main branch.

## License

This project is licensed under the [MIT License](LICENSE).
