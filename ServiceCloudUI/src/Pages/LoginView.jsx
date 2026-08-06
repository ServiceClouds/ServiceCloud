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

            <div className="login-left">

                <div className="brand">
                    <h1>ServiceCloud</h1>
                    <p>Multi-Tenant Management System</p>
                </div>

                <div className="welcome">
                    <h2>Welcome Back</h2>
                    <p>Enter your email address to continue.</p>
                </div>

                <div className="form-group">

                    <label>Email Address</label>

                    <input
                        type="email"
                        placeholder="admin@servicecloud.com"
                        value={email}
                        onChange={(e) => setEmail(e.target.value)}
                    />

                    {error && (
                        <p className="error">
                            {error}
                        </p>
                    )}

                </div>

                <button
                    className="btn-login"
                    onClick={handleContinue}
                    disabled={loading}
                >
                    {loading ? "Please wait..." : "Continue"}
                </button>

                <div className="progress">
                    <div className="circle active"></div>
                    <div className="line"></div>
                    <div className="circle"></div>
                    <div className="line"></div>
                    <div className="circle"></div>
                </div>

                <div className="progress-text">
                    <span>Email</span>
                    <span>Company</span>
                    <span>Branch</span>
                </div>

            </div>

            <div className="login-right">

                <h2>Secure Enterprise Platform</h2>

                <p>
                    Access your company workspace securely using
                    ServiceCloud Authentication.
                </p>

            </div>

        </div>

    );

}

export default LoginView;