// components/GoogleLoginButton.js
import { GoogleLogin } from "@react-oauth/google";
import { toast } from "react-hot-toast";
import { useLocation, useNavigate } from "react-router-dom";
import { useContext } from "react";
import { UserContext } from "../../context/UserContext";
import axios from "../../utils/axios";

const GoogleLoginButton = () => {
  const { login } = useContext(UserContext);
  const location = useLocation();
  const navigate = useNavigate();
  const from = location.state?.from?.pathname || "/";

  const handleGoogleLoginSuccess = async (response) => {
    const { credential } = response;
    try {
      const res = await axios.post("/authentications/sign-in-google", {
        idToken: credential,
      });
      if (res.data) {
        toast.success("Login successfully");
        localStorage.setItem("accessToken", res.data.data.accessToken);
        localStorage.setItem("refreshToken", res.data.data.refreshToken);
        login(res.data.data);
        navigate(from, { replace: true });
      }
    } catch (error) {
      toast.error("Google login failed");
      console.error("Google login error:", error);
    }
  };

  const handleGoogleLoginFailure = (error) => {
    toast.error("Google login failed");
    console.error("Google login error:", error);
  };

  return (
    <div className="flex justify-center mt-4">
      <GoogleLogin
        onSuccess={handleGoogleLoginSuccess}
        onFailure={handleGoogleLoginFailure}
      />
    </div>
  );
};

export default GoogleLoginButton;
