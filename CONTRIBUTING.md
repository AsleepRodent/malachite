# Contributing to Malachite

Thank you for your interest in contributing. To maintain the project's high standards of consistency, performance, and visual clarity, all contributions must adhere to these rules.

## Code Style

### 1. No Comments Policy
The codebase is strictly **comment-free**.
- Code must be self-documenting through clear architecture and naming.
- **NEVER** add explanatory comments, documentation tags (`///`), or commented-out code.
- Pull Requests containing any form of comments will be rejected.

### 2. Naming Conventions
- **Internal Variables**: Use `camelCase` without underscores (e.g., `lastTime`, not `_lastTime` or `last_time`).
- **Public/Accessible Members**: Use `PascalCase` for any variable, property, or method accessible by other classes or modules.
- **Minimalism**: Prioritize short, single-word names (e.g., `Start`, `Run`, `Fire`, `Target`).
- **Conceptual Consistency**: Always use the same term for the same concept. For example, if an action affects an object, use `target` (e.g., `Delete(target)`) consistently across the entire engine.

### 3. Architecture & Performance
- **File-Scoped Namespaces**: Use `namespace Malachite.Example;` to reduce indentation.
- **Memory Efficiency**: Use `ReadOnlySpan<T>` for high-frequency collection processing to minimize allocations.
- **Early Returns**: Use early returns to keep logic flat and readable.
- **Single Responsibility**: One class per file, focused on a single task.

## Workflow
1. Fork the repository.
2. Create a feature branch.
3. Ensure the code compiles under **.NET 9.0** on Linux.
4. Submit a Pull Request.

By contributing to Malachite, you agree to follow these "comment-free" and naming standards strictly.
