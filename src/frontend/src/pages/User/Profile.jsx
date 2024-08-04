import React from "react";
import Information from "./component/Information";

const Profile = () => {
  return (
      <div className="p-12 flex justify-center items-start gap-6 bg-slate-100">
        <div className="w-[720px]">
          <Information />
        </div>
      </div>
  );
};

export default Profile;
