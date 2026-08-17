import "./Login.css";

function LoginView({
    email,
    setEmail,
    loading,
    error,
    handleContinue
}) {
    return (
        <div className="login-page">

            {/* LEFT BRANDING PANEL */}
            <div className="login-brand-panel">

                <div className="brand-content">

                    <div className="brand-logo">
                        <span className="brand-logo-icon">S</span>

                        <div>
                            <h1>ServiceCloud</h1>
                            <span>Enterprise Management Platform</span>
                        </div>
                    </div>

                    <div className="brand-message">
                        <span className="eyebrow">WELCOME TO SERVICECLOUD</span>

                        <h2>
                            Everything your business needs,
                            <strong> in one place.</strong>
                        </h2>

                        <p>
                            Manage your companies, branches, staff and services
                            through one secure and centralized platform.
                        </p>
                    </div>

                    <div className="feature-list">

                        <div className="feature-item">
                            <div className="feature-icon">✓</div>
                            <div>
                                <strong>Centralized Management</strong>
                                <span>Manage your entire organization from one platform.</span>
                            </div>
                        </div>

                        <div className="feature-item">
                            <div className="feature-icon">✓</div>
                            <div>
                                <strong>Secure Access</strong>
                                <span>Enterprise-grade authentication and access control.</span>
                            </div>
                        </div>

                        <div className="feature-item">
                            <div className="feature-icon">✓</div>
                            <div>
                                <strong>Multi-Branch Support</strong>
                                <span>Work with multiple companies and branches seamlessly.</span>
                            </div>
                        </div>

                    </div>

                </div>

                <div className="brand-footer">
                    © 2026 ServiceCloud. All rights reserved.
                </div>

            </div>


            {/* RIGHT LOGIN PANEL */}
            <div className="login-form-panel">

                <div className="login-form-wrapper">

                    <div className="mobile-brand">
                        <div className="brand-logo">
                            <span className="brand-logo-icon">S</span>

                            <div>
                                <h1>ServiceCloud</h1>
                                <span>Enterprise Platform</span>
                            </div>
                        </div>
                    </div>

                    <div className="login-heading">

                        <span className="form-eyebrow">
                            ACCOUNT ACCESS
                        </span>

                        <h2>Welcome back</h2>

                        <p>
                            Enter your email address to continue to your workspace.
                        </p>

                    </div>

                    <div className="login-form">

                        <div className="form-group">

                            <label htmlFor="email">
                                Email address
                            </label>

                            <div className="input-wrapper">

                                <span className="input-icon">
                                    @
                                </span>

                                <input
                                    id="email"
                                    type="email"
                                    placeholder="name@company.com"
                                    value={email}
                                    onChange={(e) => setEmail(e.target.value)}
                                    onKeyDown={(e) => {
                                        if (e.key === "Enter") {
                                            handleContinue();
                                        }
                                    }}
                                />

                            </div>

                        </div>

                        {error && (
                            <div className="login-error">
                                <span>!</span>
                                {error}
                            </div>
                        )}

                        <button
                            className="btn-login"
                            onClick={handleContinue}
                            disabled={loading || !email.trim()}
                        >
                            {loading ? (
                                <>
                                    <span className="spinner"></span>
                                    Checking account...
                                </>
                            ) : (
                                <>
                                    Continue
                                    <span className="button-arrow">→</span>
                                </>
                            )}
                        </button>

                    </div>


                    {/* LOGIN PROGRESS */}
                    <div className="login-progress">

                        <div className="progress-step active">
                            <div className="progress-circle">1</div>
                            <span>Email</span>
                        </div>

                        <div className="progress-line"></div>

                        <div className="progress-step">
                            <div className="progress-circle">2</div>
                            <span>Company</span>
                        </div>

                        <div className="progress-line"></div>

                        <div className="progress-step">
                            <div className="progress-circle">3</div>
                            <span>Branch</span>
                        </div>

                    </div>

                    <p className="security-note">
                        🔒 Your information is securely encrypted and protected.
                    </p>

                </div>

            </div>

        </div>
    );
}

export default LoginView;