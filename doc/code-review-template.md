# Code Review Form: [Title]
*[subtitle / release]*

Date: **[now]**

Reviewers: **[A,B,C]**

Items: **JIRA-ID, GIT-ID**

Outcome: **[PASS | REJECT]** / **[grade]**

 *[one liner status + code]*

Grade | Topic
-- | --
[ ] | Code Quality / Formatting
[ ] | Documentation / Comments
[ ] | Design / Architecture
[ ] | Reliability 
[ ] | Maintainability 
[ ] | Testing / Coverage
[ ] | Performance / Scalability
[ ] | Security
[ ] | Fitness for business purpose

> *Grade scale: 1-10  (0=failure / 1=the worst / 10=the best)*
---

## Aspects


### Code Quality / Formatting
- Inconsistent entity naming (objects, db, etc.)
- Misspelled identifiers
- Non adherence to established naming standards (arch)
- Formatting, tab, indentation, comment adherence to common standards
- Standardized file naming
- Cyclomatic complexity
- Code repetition
- Unclear naming, lack of explicitness, and difficult meaning derivation exists
- Number of warnings

### Documentation / Comments
- Code XML comments on public entities
- API controllers built-in documentation
- Project manual markdown
- Quick start guide
- Developer setup instructions

### Design / Architecture
- Coding per Feature ratio (code, config, infrastructure) - should be low
- SOLID adherence
- Separation of concerns
- Logic repetition
- Use of app modules vs business-rule-dependent strategies
- Use of integration port plugins where required
- Excessive complexity: OOP/svc/solution
- Industry standard patterns
- Future Extensibility
- Requirement change malleability
- Excessive relational object dependencies and complex joining of data
- Bad data models: no indexes, violation of normalization rules in RDBMS
- Testability (entity factoring for mocking, UI control hooks etc..)
- 3rd party dependencies (framework/package counts; lower - the better)

### Modifiability

### Portability

### Extensibility

### Conceptual Integrity

### Interoperability

### Usability

### Reusability

### Maintainability
- Level at which the code exceeds a single or minimal set of responsibilities
- Amount of code that handles inappropriate responsibilities (UI Formatting)
- Level of inter-dependencies, package/3rd party dependencies
- Risk of side affecting changes if this or other code is modified
- Improper entity naming causes uncertainty of understanding
- DevOps readiness: config/scripting for deployment/environment migration
- Externalized config (nothing-hard-coded)
- Operational compatibility: platform/framework support
- Monitoring / Logging / Instrumentation
- Remote management / administration

### Reliability
- Appropriate exception handling
- Fault tolerance considerations risks impacting this or other processes
- Level of issues with failure, recovery, restart, or failed state
- Level of service inter-dependencies
- SLA for failure modes
- Identify SPF

### Availability
- Automatic failover
- Distributed failure handling / domino cascade suppression e.g. circuit breaker
- Distributed leader election / Split brain reparation
- Stateless or resilient state management

### Testing and Testability
- Unit testing
- Integration testing
- Test environments
- Test automation with DevOps
- Identify most complex logic areas and areas with frequently changing requirements and determine their testing scenario coverage adequacy

### Debug-ability /Monitoring

### Efficiency

### Performance / Scalability
- Level of resources required by this process risk affecting or being affected by systems performance
- Includes unnecessary or excessive transformations or string formatting/concat functions
- Algorithmic CPU efficiency
- Algorithmic MEMORY efficiency
- Algorithmic IO efficiency
- Parallel programming
- Asynchronous programming
- Extra copy avoidance
- In-memory caching
- Pre-computing results
- Resilience to domino effect
- Scale-out capabilities 2x, 4x, 10x etc.
- Identify bottlenecks
- Avoid unnecessary global state

### Security
- Standard encryption mechanisms only (AES, Rijndael, HMAC256, etc..)
- API Token auth
- Principle of least privilege
- OAuth/OIDC for public services
- PKDF+ level password hashing
- Revoke access tokens
- Access audit
- Fine-grain authorization levels
- SQL Injection 1, 2+ degree
- Script Injection 1, 2+ degree
- Session Fixations
- CSRF protection
- OWASP adherence
- Identity provider
- Amount of risk to normal business operations due to security breach or attack

### Ease of Deployment

### Ease of Administration

### Development Productivity

### Fitness for Purpose
- Requirements definition level - did business provide well-defined requirements?
- Does solution provide what business needs
- SLA compliance
- Architecture malleability for future business requirement changes
- General SWOT analysis
- IP asset SDLC TCO analysis
  - ongoing human resources
  - ongoing infrastructure resources
  - licensing/other resources
  - maintainability
  - extensibility / future growth opportunities
  - consumer satisfaction
